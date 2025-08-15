using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    private Transform target; // �e�̃^�[�Q�b�g
    private float speed = 10f;

    // �^�[�Q�b�g�ƃX�s�[�h��ݒ�
    public void SetTarget(Transform targetTransform, float moveSpeed)
    {
        target = targetTransform;
        speed = moveSpeed;
    }

    private void Update()
    {
        if (target == null) return;

        // �^�[�Q�b�g�������v�Z
        Vector3 dir = (target.position - transform.position).normalized;

        // �e���^�[�Q�b�g�����Ɉړ�
        transform.position += dir * speed * Time.deltaTime;

        // �e���^�[�Q�b�g�����Ɍ�����
        transform.forward = dir;

        // �^�[�Q�b�g�ɋ߂Â�����Փˈ���
        if (Vector3.Distance(transform.position, target.position) < 0.2f)
        {
            // �G�ɗh�ꃊ�A�N�V����
            EnemyReaction reaction = target.GetComponent<EnemyReaction>();
            if (reaction != null)
            {
                reaction.Shake();
            }

            // �����Ń_���[�W�����Ȃǂ��\
            Destroy(gameObject);
        }
    }
}