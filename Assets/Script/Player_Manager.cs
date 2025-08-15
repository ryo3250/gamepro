using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //�e��Prefab
    [SerializeField] private GameObject bulletPrefab;

    //�ړ��A�N�V�����{�^��
    [SerializeField] private InputAction[] move_ = new InputAction[6];

    // ���b�N�I���ő吔
    [SerializeField] private int maxLockOnCount = 8;

    // ���b�N�I������
    [SerializeField] private float lockOnDistance = 20f;

    // ���b�N�I���L���p�x
    [SerializeField] private float lockOnAngle = 45f;

    // Z�L�[�p��InputAction
    [SerializeField] private InputAction lockOnAction;

    // ���̐F��ێ����鎫��
    private Dictionary<Renderer, Color> originalColors = new Dictionary<Renderer, Color>();

    // ���b�N�I�����̓G��Renderer��ێ�
    private List<Renderer> lockedEnemies = new List<Renderer>();

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

        lockOnAction.Enable();

        // Z�L�[������
        lockOnAction.performed += ctx =>
        {
            LockOnEnemies();
        };

        // Z�L�[�������u��
        lockOnAction.canceled += ctx =>
        {
            Debug.Log("Z�L�[�������I"); // Z�L�[�C�x���g�����Ă��邩
            foreach (Renderer rend in lockedEnemies)
            {
                Debug.Log("�e�𐶐�: " + rend.name); // ���b�N�I���G�����邩
                GameObject bullet = Instantiate(bulletPrefab, transform.position + transform.forward * 1f , Quaternion.identity);

                HomingBullet homing = bullet.AddComponent<HomingBullet>();
                homing.SetTarget(rend.transform, 10f); // 10�͒e�̑��x
            }
            ResetLockOn();
        };
    }

    // �X�V
    private void Update()
    {
        // ��]����
        transform.eulerAngles += moveAngle_;

        // �����Ă�������ɑO�i/���
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
        // ���łɃ��b�N�I�����Ă���ꍇ�͈�U����
        ResetLockOn();

        // �V�[�����̂��ׂĂ̓G���擾
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        List<(Renderer, float)> candidates = new List<(Renderer, float)>();

        foreach (GameObject enemy in enemies)
        {
            Renderer rend = enemy.GetComponent<Renderer>();
            if (!rend) continue;

            Vector3 dirToEnemy = (enemy.transform.position - transform.position).normalized;
            float angle = Vector3.Angle(transform.right, dirToEnemy);
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            // �����Ă�������E���������𖞂����G����
            if (angle <= lockOnAngle && distance <= lockOnDistance)
            {
                candidates.Add((rend, distance));
            }
        }

        // �������߂����Ƀ\�[�g
        candidates.Sort((a, b) => a.Item2.CompareTo(b.Item2));

        // �ő� maxLockOnCount �̂܂Ń��b�N�I��
        for (int i = 0; i < Mathf.Min(maxLockOnCount, candidates.Count); i++)
        {
            Renderer rend = candidates[i].Item1;

            // ���̐F��ۑ�
            originalColors[rend] = rend.material.color;

            // �ԐF�ɕύX
            rend.material.color = Color.red;

            // ���b�N�I�������X�g�ɒǉ�
            lockedEnemies.Add(rend);
        }
    }

    private void ResetLockOn()
    {
        foreach (Renderer rend in lockedEnemies)
        {
            if (originalColors.ContainsKey(rend))
            {
                rend.material.color = originalColors[rend]; // ���̐F�ɖ߂�
            }
        }
        lockedEnemies.Clear();
        originalColors.Clear();
    }

    // ���̓A�N�V�����̃Z�b�g�A�b�v
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

