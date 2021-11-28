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
            //Alpha값을 1에서 0으로 : fade Out
            yield return StartCoroutine(FadeEffect(1, 0));
            //Alpha값을 0에서 1으로 : fade In
            yield return StartCoroutine(FadeEffect(1, 0));

        }
    }
    private IEnumerator FadeEffect(float start, float end)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;

        while (percent < 1)
        {
            //fadeTime시간동안 while() 반복문 실행
            currentTime += Time.deltaTime;
            percent = currentTime / fadeTime;

            // percent는 0-> 1로 증가, 그 값에 따라 Alpha변화시킴
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
