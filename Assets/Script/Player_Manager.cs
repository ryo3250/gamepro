using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    /// <summary>
    /// 弾のPrefab
    /// </summary>
    [SerializeField] private GameObject bulletPrefab_;

    /// <summary>
    /// 発射アクションボタン
    /// </summary>
    [SerializeField] private InputAction fire_;

    /// <summary>
    /// 移動アクションボタン
    /// </summary>
    [SerializeField] private InputAction[] move_ = new InputAction[4];

    /// <summary>
    /// 毎フレームの移動角度
    /// </summary>
    private Vector3 moveAngle_;

    /// <summary>
    /// カタパルトオブジェクト
    /// </summary>
    private GameObject catapult_;

    /// <summary>
    /// 弾オブジェクト
    /// </summary>
    //private Bullet bullet_;

    /// <summary>
    /// 開始処理
    /// </summary>
    private void Start()
    {
        SetupInputActions();
        FindCatapult();
        //InstantiateBullet();
    }

    /// <summary>
    /// 更新
    /// </summary>
    private void Update()
    {
        // 角度を加える
        transform.eulerAngles += moveAngle_;
    }

    /// <summary>
    /// 後更新
    /// </summary>
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

        move_[0].canceled += ctx => moveAngle_.y = 0.0f;
        move_[1].canceled += ctx => moveAngle_.y = 0.0f;
        move_[2].canceled += ctx => moveAngle_.z = 0.0f;
        move_[3].canceled += ctx => moveAngle_.z = 0.0f;
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

