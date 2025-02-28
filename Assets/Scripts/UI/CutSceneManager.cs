using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CutSceneManager : MonoBehaviour
{
    public Image[] images;
    public Text[] texts;

    [SerializeField]
    private float fadeDuration;

    float cutTime = 0;

    private void Start()
    {
        PlayerPrefs.SetInt("Story", 0);
        foreach (Image image in images)
        {
            Color imageColor = image.color;
            imageColor.a = 0f;
            image.color = imageColor;
        }

        foreach (Text text in texts)
        {
            Color textColor = text.color;
            textColor.a = 0f;
            text.color = textColor;
        }
        
        StartCoroutine(FadeCut());
    }

    IEnumerator FadeCut()
    {
        if (PlayerPrefs.GetInt("Story", 0) == 0)
        {
            for (int i = 0; i < images.Length; i++)
            {
                StartCoroutine(FadeIn(images[i], texts[i]));
                yield return new WaitForSeconds(fadeDuration + 4);
                StartCoroutine(FadeOut(images[i], texts[i]));
                yield return new WaitForSeconds(fadeDuration);
            }
            PlayerPrefs.SetInt("Story", 1);
        }
        SceneManager.LoadScene("StartScene");
    }

    IEnumerator FadeIn(Image image, Text text)
    {
        Color imageColor = image.color;
        Color textColor = text.color;

        while (cutTime <= fadeDuration)
        {
            cutTime += Time.deltaTime;
            imageColor.a = cutTime / fadeDuration;
            image.color = imageColor;
            textColor.a = cutTime / fadeDuration;
            text.color = textColor;
            yield return null;
        }

        cutTime = 0f;
    }

    IEnumerator FadeOut(Image image, Text text)
    {
        Color imageColor = image.color;
        Color textColor = text.color;

        while (cutTime <= fadeDuration)
        {
            cutTime += Time.deltaTime;
            imageColor.a = 1 - cutTime / fadeDuration;
            image.color = imageColor;
            textColor.a = 1 - cutTime / fadeDuration;
            text.color = textColor;
            yield return null;
        }

        cutTime = 0f;
    }
}
