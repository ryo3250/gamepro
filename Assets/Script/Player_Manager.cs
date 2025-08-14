using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // ロックオン距離
    [SerializeField] private float lockOnRange = 10f;

    // 前方コーン角度
    [SerializeField] private float lockOnAngle = 45f;

    // 敵レイヤー
    [SerializeField] private LayerMask enemyLayer;

    //弾のPrefab
    [SerializeField] private GameObject bulletPrefab_;

    // 発射アクションボタン
    [SerializeField] private InputAction fire_;

    //移動アクションボタン
    [SerializeField] private InputAction[] move_ = new InputAction[6];

    // ロックオン中の敵のRendererを保持するリスト
    private List<Renderer> lockedEnemies = new List<Renderer>();

    // ロックオン前の敵の色を保持する辞書（元の色に戻すために使用）
    private Dictionary<Renderer, Color> originalColors = new Dictionary<Renderer, Color>();

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
        FindCatapult();
        //InstantiateBullet();
    }

    // 更新
    private void Update()
    {
        // 回転処理
        transform.eulerAngles += moveAngle_;

        // 向いている方向に前進/後退
        Vector3 moveDir = Vector3.right * moveInputZ_.z;
        transform.Translate(moveDir * 0.1f, Space.Self);

        if () 
        { 
        
        }
    }



    // 後更新
    //private void LateUpdate()
    //{
    //    if (!bullet_)
    //    {
    //        return;
    //    }

    //    GameController.instance.CanHitEnemy(bullet_);
    //}


    /// <summary>
    /// 入力アクションのセットアップ
    /// </summary>
    private void SetupInputActions()
    {
        fire_.Enable();
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

    /// <summary>
    /// 発射ボタンが押されたときの処理
    /// </summary>
    /// <param name="ctx"></param>
    //private void OnFirePerformed(InputAction.CallbackContext ctx)
    //{
    //    bullet_.transform.SetParent(null);
    //    bullet_.Fire(catapult_.transform.forward);

    //    bullet_ = null;

    //    // 次弾を装填する
    //    LoadBullet();
    //}

    /// <summary>
    /// 次弾を装填する
    /// </summary>
    //private async void LoadBullet()
    //{
    //    // 1秒待つ
    //    await Awaitable.WaitForSecondsAsync(1f);

    //    // 弾を生成する
    //    InstantiateBullet();
    //}

    /// <summary>
    /// Catapult を探す
    /// </summary>
    private void FindCatapult()
    {
        // Player の子供から Catapult を探す
        catapult_ = transform.Find("Catapult")?.gameObject;
    }

    /// <summary>
    /// Bullet をインスタンス化する
    /// </summary>
    //private void InstantiateBullet()
    //{
    //    // Prefab の Bullet を生成してアタッチする
    //    if (bulletPrefab_ != null)
    //    {
    //        bullet_ = Instantiate(bulletPrefab_, catapult_.transform)?.GetComponent<Bullet>();
    //    }
    //}
}

