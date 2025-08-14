using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; //�q�G�����L�[�ɒu���G�́u���f�[�^�iPrefab�j�v������ϐ�
    public int enemyCount = 24; //��������G�̐�
    public Vector3 spawnArea = new Vector3(10f, 0f, 10f); //�G���o��������͈�

    void Start()
    {
        for (int i = 0; i < enemyCount; i++) //for ���� enemyCount ��J��Ԃ�
        {
            Vector3 pos = new Vector3(
                Random.Range(-spawnArea.x, spawnArea.x),
                Random.Range(0f, 2f), // �����������_��
                Random.Range(-spawnArea.z, spawnArea.z)
            );

            Instantiate(enemyPrefab, pos, Quaternion.identity);//�w�肵��Prefab�ienemyPrefab�j�� pos �̈ʒu�ɐ���
        }
    }
}
