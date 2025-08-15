using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //弾のPrefab
    [SerializeField] private GameObject bulletPrefab;

    //移動アクションボタン
    [SerializeField] private InputAction[] move_ = new InputAction[6];

    // ロックオン最大数
    [SerializeField] private int maxLockOnCount = 8;

    // ロックオン距離
    [SerializeField] private float lockOnDistance = 20f;

    // ロックオン有効角度
    [SerializeField] private float lockOnAngle = 45f;

    // Zキー用のInputAction
    [SerializeField] private InputAction lockOnAction;

    // 元の色を保持する辞書
    private Dictionary<Renderer, Color> originalColors = new Dictionary<Renderer, Color>();

    // ロックオン中の敵のRendererを保持
    private List<Renderer> lockedEnemies = new List<Renderer>();

    // 毎フレームの移動角度
    private Vector3 moveAngle_;

    // 毎フレームの移動
    private Vector3 moveInputZ_;

    // カタパルトオブジェクト
    private GameObject catapult_;

    // 弾オブジェクト
    //private Bullet bullet_;

    // 開始処理
    private void Start()
    {
        SetupInputActions();

        lockOnAction.Enable();

        // Zキー押下時
        lockOnAction.performed += ctx =>
        {
            LockOnEnemies();
        };

        // Zキー離した瞬間
        lockOnAction.canceled += ctx =>
        {
            Debug.Log("Zキー離した！"); // Zキーイベントが来ているか
            foreach (Renderer rend in lockedEnemies)
            {
                Debug.Log("弾を生成: " + rend.name); // ロックオン敵がいるか
                GameObject bullet = Instantiate(bulletPrefab, transform.position + transform.forward * 1f , Quaternion.identity);

                HomingBullet homing = bullet.AddComponent<HomingBullet>();
                homing.SetTarget(rend.transform, 10f); // 10は弾の速度
            }
            ResetLockOn();
        };
    }

    // 更新
    private void Update()
    {
        // 回転処理
        transform.eulerAngles += moveAngle_;

        // 向いている方向に前進/後退
        Vector3 moveDir = Vector3.right * moveInputZ_.z;
        transform.Translate(moveDir * 0.1f, Space.Self);

        if (lockOnAction.ReadValue<float>() > 0)
        {
            LockOnEnemies();
        }
        else 
        {
            ResetLockOn();
        }
    }

    private void LockOnEnemies()
    {
        // すでにロックオンしている場合は一旦解除
        ResetLockOn();

        // シーン内のすべての敵を取得
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        List<(Renderer, float)> candidates = new List<(Renderer, float)>();

        foreach (GameObject enemy in enemies)
        {
            Renderer rend = enemy.GetComponent<Renderer>();
            if (!rend) continue;

            Vector3 dirToEnemy = (enemy.transform.position - transform.position).normalized;
            float angle = Vector3.Angle(transform.right, dirToEnemy);
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            // 向いている方向・距離条件を満たす敵だけ
            if (angle <= lockOnAngle && distance <= lockOnDistance)
            {
                candidates.Add((rend, distance));
            }
        }

        // 距離が近い順にソート
        candidates.Sort((a, b) => a.Item2.CompareTo(b.Item2));

        // 最大 maxLockOnCount 体までロックオン
        for (int i = 0; i < Mathf.Min(maxLockOnCount, candidates.Count); i++)
        {
            Renderer rend = candidates[i].Item1;

            // 元の色を保存
            originalColors[rend] = rend.material.color;

            // 赤色に変更
            rend.material.color = Color.red;

            // ロックオン中リストに追加
            lockedEnemies.Add(rend);
        }
    }

    private void ResetLockOn()
    {
        foreach (Renderer rend in lockedEnemies)
        {
            if (originalColors.ContainsKey(rend))
            {
                rend.material.color = originalColors[rend]; // 元の色に戻す
            }
        }
        lockedEnemies.Clear();
        originalColors.Clear();
    }

    // 入力アクションのセットアップ
    private void SetupInputActions()
    {
        //fire_.performed += OnFirePerformed;

        for (int i = 0; i < move_.Length; i++)
        {
            move_[i].Enable();
        }
        move_[0].performed += ctx => moveAngle_.y = 0.1f;
        move_[1].performed += ctx => moveAngle_.y = -0.1f;
        move_[2].performed += ctx => moveAngle_.z = 0.1f;
        move_[3].performed += ctx => moveAngle_.z = -0.1f;
        move_[4].performed += ctx => moveInputZ_.z = 1f;
        move_[5].performed += ctx => moveInputZ_.z = -1f;

        move_[0].canceled += ctx => moveAngle_.y = 0.0f;
        move_[1].canceled += ctx => moveAngle_.y = 0.0f;
        move_[2].canceled += ctx => moveAngle_.z = 0.0f;
        move_[3].canceled += ctx => moveAngle_.z = 0.0f;
        move_[4].canceled += ctx => moveInputZ_.z = 0f;
        move_[5].canceled += ctx => moveInputZ_.z = 0f;

    }

}

