using UnityEngine;

public class BulletMover : MonoBehaviour
{
    private Transform target;    // �e���ǔ�����G
    private float speed = 10f;   // �e�̑��x

    // �^�[�Q�b�g�ƃX�s�[�h���Z�b�g����֐�
    public void SetTarget(Transform targetTransform, float moveSpeed)
    {
        target = targetTransform;
        speed = moveSpeed;
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject); // �^�[�Q�b�g����������e������
            return;
        }

        // �^�[�Q�b�g�������v�Z
        Vector3 dir = (target.position - transform.position).normalized;

        // �e���^�[�Q�b�g�����Ɉړ�
        transform.position += dir * speed * Time.deltaTime;

        // �e���^�[�Q�b�g�����Ɍ�����
        transform.forward = dir;

        // �^�[�Q�b�g�ɋ߂Â�����Փˈ����ɂ��ď���
        if (Vector3.Distance(transform.position, target.position) < 0.2f)
        {
            // �����Ƀ_���[�W�����Ȃǂ����邱�Ƃ��\
            Destroy(gameObject);
        }
    }
}
