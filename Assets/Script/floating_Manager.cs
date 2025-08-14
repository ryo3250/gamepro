using UnityEngine;

public class EnemyFloat : MonoBehaviour
{
    public float waveHeight = 0.5f;    // ã‰º—h‚ê‚Ì•
    public float waveSpeed = 2f;       // ã‰º—h‚ê‚Ì‘¬‚³
    public float rotateSpeed = 30f;    // ‰ñ“]‘¬“x

    private Vector3 startPos;          // ‰ŠúˆÊ’u
    private float waveOffset;          // ƒTƒCƒ“”g‚ÌˆÊ‘Š‚¸‚ç‚µ

    void Start()
    {
        startPos = transform.position;
        waveOffset = Random.Range(0f, Mathf.PI * 2f); // ŒÂ‘Ì‚²‚Æ‚É—h‚êŠJn‚ğ‚¸‚ç‚·
    }

    void Update()
    {
        // ƒTƒCƒ“”g‚Åã‰º‚É—h‚ç‚·
        float wave = Mathf.Sin(Time.time * waveSpeed + waveOffset) * waveHeight;
        Vector3 floatPos = startPos;
        floatPos.y += wave;
        transform.position = floatPos;

        // Y²‰ñ“]
        transform.eulerAngles += new Vector3(0, rotateSpeed * Time.deltaTime, 0);
    }
}
