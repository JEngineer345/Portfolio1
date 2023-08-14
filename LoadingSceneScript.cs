using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneScript : MonoBehaviour
{
    [SerializeField] Slider slider;

    private float time;

    void Start()
    {
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("MainScene");
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            time += Time.deltaTime;
            slider.value = time / 10f;
            if (time > 10)
            {
                operation.allowSceneActivation = true;
            }
        }

        yield return null;
    }
}