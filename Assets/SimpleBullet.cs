using UnityEngine;

public class SimpleBullet : MonoBehaviour
{
    public float speed = 10f;

    private void Update()
    {
        // ‘O•û‚ÉˆÚ“®
        transform.position += transform.right * speed * Time.deltaTime;

        // ‚à‚µ‰“‚­‚És‚«‚·‚¬‚½‚çÁ‚·
        if (Vector3.Distance(Vector3.zero, transform.position) > 50f)
        {
            Destroy(gameObject);
        }
    }
}