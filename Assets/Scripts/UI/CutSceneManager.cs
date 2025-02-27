using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutSceneManager : MonoBehaviour
{
    public Image[] images; // 배경 이미지 → 얘를 페이드인/아웃
    public Text[] texts;
    public float fadeDuration = 1f;
    public float displayTime = 2f;

    private void Start()
    {
        foreach (Image image in images)
        {
            Color color = image.color;
            color.a = 0;
            image.color = color;
        }

        foreach (Text text in texts)
        {
            Color textColor = text.color;
            textColor.a = 0;
            text.color = textColor;
        }

        // 페이드 인 / 아웃 순차적으로 코루틴 실행
        StartCoroutine(FadeImages());
    }

    IEnumerator FadeImages()
    {
        for (int i = 0; i < images.Length; i++)
        {

            StartCoroutine(FadeIn(images[i], texts[i]));
            yield return new WaitForSeconds(fadeDuration);
            StartCoroutine(FadeOut(images[i], texts[i]));
            yield return new WaitForSeconds(fadeDuration);
        }
    }

    IEnumerator FadeIn(Image image, Text text)
    {
        float elapsedTime = 0;
        Color color = image.color;
        Color textColor = text.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = elapsedTime / fadeDuration;
            image.color = color;
            textColor.a = elapsedTime / fadeDuration;
            text.color = color;
        }
        yield return new WaitForSeconds(fadeDuration);
    }

    IEnumerator FadeOut(Image image, Text text)
    {
        float elapsedTime = 0;
        Color color = image.color;
        Color textColor = text.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = elapsedTime / fadeDuration;
            image.color = color;
            textColor.a = elapsedTime / fadeDuration;
            text.color = color;
        }
        yield return new WaitForSeconds(fadeDuration);
    }
}
