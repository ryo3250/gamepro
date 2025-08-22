using UnityEngine;
using System.Collections;

public class EnemyReaction : MonoBehaviour
{
    private Vector3 originalPos;
    private Color originalColor;
    private Renderer rend;

    // 揺れる強さ
    public float shakeAmount = 0.2f;

    // 揺れる速さ
    public float shakeSpeed = 20f;

    // 揺れる時間
    public float shakeDuration = 0.5f;

    public Color hitColor = Color.yellow;


    void Start()
    {
        originalPos = transform.localPosition;
        rend = GetComponent<Renderer>();
        if (rend != null)
        {
            originalColor = rend.material.color;
        }
    }

    public void Shake()
    {
        StartCoroutine(ShakeCoroutine());
    }

    private IEnumerator ShakeCoroutine()
    {
        float elapsed = 0f;

        // 色を黄色に変更
        if (rend != null)
        {
            rend.material.color = hitColor;
        }

        while (elapsed < shakeDuration)
        {
            transform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
            elapsed += Time.deltaTime;
            yield return null;
        }

        // 元の位置に戻す
        transform.localPosition = originalPos;

        // 元の色に戻す
        if (rend != null)
        {
            rend.material.color = originalColor;
        }
    }
}