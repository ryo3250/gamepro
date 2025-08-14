using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // ���b�N�I������
    [SerializeField] private float lockOnRange = 10f;

    // �O���R�[���p�x
    [SerializeField] private float lockOnAngle = 45f;

    // �G���C���[
    [SerializeField] private LayerMask enemyLayer;

    //�e��Prefab
    [SerializeField] private GameObject bulletPrefab_;

    // ���˃A�N�V�����{�^��
    [SerializeField] private InputAction fire_;

    //�ړ��A�N�V�����{�^��
    [SerializeField] private InputAction[] move_ = new InputAction[6];

    // ���b�N�I�����̓G��Renderer��ێ����郊�X�g
    private List<Renderer> lockedEnemies = new List<Renderer>();

    // ���b�N�I���O�̓G�̐F��ێ����鎫���i���̐F�ɖ߂����߂Ɏg�p�j
    private Dictionary<Renderer, Color> originalColors = new Dictionary<Renderer, Color>();

    // ���t���[���̈ړ��p�x
    private Vector3 moveAngle_;

    // ���t���[���̈ړ�
    private Vector3 moveInputZ_;

    // �J�^�p���g�I�u�W�F�N�g
    private GameObject catapult_;

    // �e�I�u�W�F�N�g
    //private Bullet bullet_;

    // �J�n����
    private void Start()
    {
        SetupInputActions();
        FindCatapult();
        //InstantiateBullet();
    }

    // �X�V
    private void Update()
    {
        // ��]����
        transform.eulerAngles += moveAngle_;

        // �����Ă�������ɑO�i/���
        Vector3 moveDir = Vector3.right * moveInputZ_.z;
        transform.Translate(moveDir * 0.1f, Space.Self);

        if () 
        { 
        
        }
    }



    // ��X�V
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

