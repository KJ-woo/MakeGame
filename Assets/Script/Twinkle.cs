using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Twinkle : MonoBehaviour
{
    private float fadeTime = 1.0f;
    private float minFadeTime = 1.0f;
    private float maxFadeTime = 4.0f;

    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        fadeTime = Random.Range(minFadeTime, maxFadeTime);

        StartCoroutine("TwinkleLoop");
    }

    private IEnumerator TwinkleLoop()
    {
        while (true)
        {
            //Alpha���� 1���� 0���� : fade Out
            yield return StartCoroutine(FadeEffect(1, 0));
            //Alpha���� 0���� 1���� : fade In
            yield return StartCoroutine(FadeEffect(1, 0));

        }
    }
    private IEnumerator FadeEffect(float start, float end)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;

        while (percent < 1)
        {
            //fadeTime�ð����� while() �ݺ��� ����
            currentTime += Time.deltaTime;
            percent = currentTime / fadeTime;

            // percent�� 0-> 1�� ����, �� ���� ���� Alpha��ȭ��Ŵ
            Color color = spriteRenderer.color;
            color.a = Mathf.Lerp(start, end, percent);
            spriteRenderer.color = color;

            yield return null;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}