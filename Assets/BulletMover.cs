using UnityEngine;

public class BulletMover : MonoBehaviour
{
    private Transform target;    // 弾が追尾する敵
    private float speed = 10f;   // 弾の速度

    // ターゲットとスピードをセットする関数
    public void SetTarget(Transform targetTransform, float moveSpeed)
    {
        target = targetTransform;
        speed = moveSpeed;
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject); // ターゲットが消えたら弾も消す
            return;
        }

        // ターゲット方向を計算
        Vector3 dir = (target.position - transform.position).normalized;

        // 弾をターゲット方向に移動
        transform.position += dir * speed * Time.deltaTime;

        // 弾をターゲット方向に向ける
        transform.forward = dir;

        // ターゲットに近づいたら衝突扱いにして消す
        if (Vector3.Distance(transform.position, target.position) < 0.2f)
        {
            // ここにダメージ処理などを入れることも可能
            Destroy(gameObject);
        }
    }
}
