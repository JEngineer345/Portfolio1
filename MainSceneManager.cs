using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneManager : MonoBehaviour
{
    public GameObject SettingPanel;

    AudioSource ClickSource;

    void Start()
    {
        ClickSource = GetComponent<AudioSource>();
        SettingPanel.SetActive(false);
    }

    void Update()
    {
    }

    public void GoToSettingPanel()
    {
        ClickSource.Play();
        SettingPanel.SetActive(true);
    }

    public void GoToBack()
    {
        ClickSource.Play();
        SettingPanel.SetActive(false);
    }
}
