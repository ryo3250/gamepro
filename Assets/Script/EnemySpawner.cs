using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; //ヒエラルキーに置く敵の「元データ（Prefab）」を入れる変数
    public int enemyCount = 24; //生成する敵の数
    public Vector3 spawnArea = new Vector3(10f, 0f, 10f); //敵を出現させる範囲

    void Start()
    {
        for (int i = 0; i < enemyCount; i++) //for 文で enemyCount 回繰り返す
        {
            Vector3 pos = new Vector3(
                Random.Range(-spawnArea.x, spawnArea.x),
                Random.Range(0f, 2f), // 高さもランダム
                Random.Range(-spawnArea.z, spawnArea.z)
            );

            Instantiate(enemyPrefab, pos, Quaternion.identity);//指定したPrefab（enemyPrefab）を pos の位置に生成
        }
    }
}
