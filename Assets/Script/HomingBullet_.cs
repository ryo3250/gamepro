using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    private Transform target; // 弾のターゲット
    private float speed = 10f;

    // ターゲットとスピードを設定
    public void SetTarget(Transform targetTransform, float moveSpeed)
    {
        target = targetTransform;
        speed = moveSpeed;
    }

    private void Update()
    {
        if (target == null) return;

        // ターゲット方向を計算
        Vector3 dir = (target.position - transform.position).normalized;

        // 弾をターゲット方向に移動
        transform.position += dir * speed * Time.deltaTime;

        // 弾をターゲット方向に向ける
        transform.forward = dir;

        // ターゲットに近づいたら衝突扱い
        if (Vector3.Distance(transform.position, target.position) < 0.2f)
        {
            // 敵に揺れリアクション
            EnemyReaction reaction = target.GetComponent<EnemyReaction>();
            if (reaction != null)
            {
                reaction.Shake();
            }

            // ここでダメージ処理なども可能
            Destroy(gameObject);
        }
    }
}