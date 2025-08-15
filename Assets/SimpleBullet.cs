using UnityEngine;

public class SimpleBullet : MonoBehaviour
{
    public float speed = 10f;

    private void Update()
    {
        // �O���Ɉړ�
        transform.position += transform.right * speed * Time.deltaTime;

        // ���������ɍs�������������
        if (Vector3.Distance(Vector3.zero, transform.position) > 50f)
        {
            Destroy(gameObject);
        }
    }
}