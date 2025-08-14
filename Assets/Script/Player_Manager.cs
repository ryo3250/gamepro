using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    /// <summary>
    /// �e��Prefab
    /// </summary>
    [SerializeField] private GameObject bulletPrefab_;

    /// <summary>
    /// ���˃A�N�V�����{�^��
    /// </summary>
    [SerializeField] private InputAction fire_;

    /// <summary>
    /// �ړ��A�N�V�����{�^��
    /// </summary>
    [SerializeField] private InputAction[] move_ = new InputAction[6];
    //[SerializeField] private float moveSpeed = 5f;

    /// <summary>
    /// ���t���[���̈ړ��p�x
    /// </summary>
    private Vector3 moveAngle_;

    private Vector3 moveInputZ_;

    /// <summary>
    /// �J�^�p���g�I�u�W�F�N�g
    /// </summary>
    private GameObject catapult_;

    /// <summary>
    /// �e�I�u�W�F�N�g
    /// </summary>
    //private Bullet bullet_;

    /// <summary>
    /// �J�n����
    /// </summary>
    private void Start()
    {
        SetupInputActions();
        FindCatapult();
        //InstantiateBullet();
    }

    /// <summary>
    /// �X�V
    /// </summary>
    private void Update()
    {
        // ��]����
        transform.eulerAngles += moveAngle_;

        // �����Ă�������ɑO�i/���
        Vector3 moveDir = Vector3.right * moveInputZ_.z;
        transform.Translate(moveDir * 0.1f, Space.Self);
    }


    /// <summary>
    /// ��X�V
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
    /// ���̓A�N�V�����̃Z�b�g�A�b�v
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
    /// ���˃{�^���������ꂽ�Ƃ��̏���
    /// </summary>
    /// <param name="ctx"></param>
    //private void OnFirePerformed(InputAction.CallbackContext ctx)
    //{
    //    bullet_.transform.SetParent(null);
    //    bullet_.Fire(catapult_.transform.forward);

    //    bullet_ = null;

    //    // ���e�𑕓U����
    //    LoadBullet();
    //}

    /// <summary>
    /// ���e�𑕓U����
    /// </summary>
    //private async void LoadBullet()
    //{
    //    // 1�b�҂�
    //    await Awaitable.WaitForSecondsAsync(1f);

    //    // �e�𐶐�����
    //    InstantiateBullet();
    //}

    /// <summary>
    /// Catapult ��T��
    /// </summary>
    private void FindCatapult()
    {
        // Player �̎q������ Catapult ��T��
        catapult_ = transform.Find("Catapult")?.gameObject;
    }

    /// <summary>
    /// Bullet ���C���X�^���X������
    /// </summary>
    //private void InstantiateBullet()
    //{
    //    // Prefab �� Bullet �𐶐����ăA�^�b�`����
    //    if (bulletPrefab_ != null)
    //    {
    //        bullet_ = Instantiate(bulletPrefab_, catapult_.transform)?.GetComponent<Bullet>();
    //    }
    //}
}

