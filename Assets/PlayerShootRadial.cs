using System.Collections.Generic;
using UnityEngine;

public class PlayerShootRadial : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;   // 弾のPrefab（HomingBullet付き）
    [SerializeField] private Transform firePoint;       // 発射位置
    [SerializeField] private float bulletSpeed = 10f;   // 弾の速度
    [SerializeField] private int radialCount = 8;       // 放射状の弾の数

    // ロックオンしている敵リスト（ロックオン処理から渡す）
    public List<Transform> lockedOnEnemies = new List<Transform>();

    private void Update()
    {
        // Zキーを離した瞬間に発射
        if (Input.GetKeyUp(KeyCode.Z))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (bulletPrefab == null || firePoint == null) return;

        // ①ロックオンしている敵へ追尾弾を撃つ
        foreach (var enemy in lockedOnEnemies)
        {
            if (enemy == null) continue;

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            HomingBullet hb = bullet.GetComponent<HomingBullet>();
            if (hb != null)
            {
                hb.SetTarget(enemy, bulletSpeed);
            }
        }

        // ②追加で「放射状の弾」を撃つ
        float angleStep = 360f / radialCount;
        for (int i = 0; i < radialCount; i++)
        {
            float angle = i * angleStep;
            Vector3 dir = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad));

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.LookRotation(dir));
            HomingBullet hb = bullet.GetComponent<HomingBullet>();
            if (hb != null)
            {
                hb.SetTarget(null, bulletSpeed); // ターゲットなし＝直進
            }
        }

        // 発射後はロックオン解除（必要なら）
        lockedOnEnemies.Clear();
    }
}
