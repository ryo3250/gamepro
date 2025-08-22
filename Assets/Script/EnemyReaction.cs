using UnityEngine;
using System.Collections;

public class EnemyReaction : MonoBehaviour
{
    private Vector3 originalPos;
    private Color originalColor;
    private Renderer rend;

    // �h��鋭��
    public float shakeAmount = 0.2f;

    // �h��鑬��
    public float shakeSpeed = 20f;

    // �h��鎞��
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

        // �F�����F�ɕύX
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

        // ���̈ʒu�ɖ߂�
        transform.localPosition = originalPos;

        // ���̐F�ɖ߂�
        if (rend != null)
        {
            rend.material.color = originalColor;
        }
    }
}