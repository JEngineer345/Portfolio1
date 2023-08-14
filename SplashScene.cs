using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplashScene : MonoBehaviour
{
    [SerializeField]
    [Range(0.01f, 10f)]
    private float fadeTime;
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
        StartCoroutine(Fade(1, 0));
    }

    IEnumerator Fade(float start, float end)
    {
        float currentTime = 0;
        float percent = 0;

        while(percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / fadeTime;

            Color color = image.color;
            color.a = Mathf.Lerp(start, end, percent);
            image.color = color;

            yield return null;
        }
    }
}
