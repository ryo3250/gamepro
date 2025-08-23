using System.Collections.Generic;
using UnityEngine;

public class PlayerShootRadial : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;   // �e��Prefab�iHomingBullet�t���j
    [SerializeField] private Transform firePoint;       // ���ˈʒu
    [SerializeField] private float bulletSpeed = 10f;   // �e�̑��x
    [SerializeField] private int radialCount = 8;       // ���ˏ�̒e�̐�

    // ���b�N�I�����Ă���G���X�g�i���b�N�I����������n���j
    public List<Transform> lockedOnEnemies = new List<Transform>();

    private void Update()
    {
        // Z�L�[�𗣂����u�Ԃɔ���
        if (Input.GetKeyUp(KeyCode.Z))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (bulletPrefab == null || firePoint == null) return;

        // �@���b�N�I�����Ă���G�֒ǔ��e������
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

        // �A�ǉ��Łu���ˏ�̒e�v������
        float angleStep = 360f / radialCount;
        for (int i = 0; i < radialCount; i++)
        {
            float angle = i * angleStep;
            Vector3 dir = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad));

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.LookRotation(dir));
            HomingBullet hb = bullet.GetComponent<HomingBullet>();
            if (hb != null)
            {
                hb.SetTarget(null, bulletSpeed); // �^�[�Q�b�g�Ȃ������i
            }
        }

        // ���ˌ�̓��b�N�I�������i�K�v�Ȃ�j
        lockedOnEnemies.Clear();
    }
}
