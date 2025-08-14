using UnityEngine;

public class EnemyFloat : MonoBehaviour
{
    public float waveHeight = 0.5f;    // �㉺�h��̕�
    public float waveSpeed = 2f;       // �㉺�h��̑���
    public float rotateSpeed = 30f;    // ��]���x

    private Vector3 startPos;          // �����ʒu
    private float waveOffset;          // �T�C���g�̈ʑ����炵

    void Start()
    {
        startPos = transform.position;
        waveOffset = Random.Range(0f, Mathf.PI * 2f); // �̂��Ƃɗh��J�n�����炷
    }

    void Update()
    {
        // �T�C���g�ŏ㉺�ɗh�炷
        float wave = Mathf.Sin(Time.time * waveSpeed + waveOffset) * waveHeight;
        Vector3 floatPos = startPos;
        floatPos.y += wave;
        transform.position = floatPos;

        // Y����]
        transform.eulerAngles += new Vector3(0, rotateSpeed * Time.deltaTime, 0);
    }
}
