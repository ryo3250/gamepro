using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int enemyCount = 24;
    public Vector3 spawnArea = new Vector3(10f, 0f, 10f);

    void Start()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            Vector3 pos = new Vector3(
                Random.Range(-spawnArea.x, spawnArea.x),
                Random.Range(0f, 2f), // ‚‚³‚àƒ‰ƒ“ƒ_ƒ€
                Random.Range(-spawnArea.z, spawnArea.z)
            );

            Instantiate(enemyPrefab, pos, Quaternion.identity);
        }
    }
}
