using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Firebase.Database;

public class PlayerButton_Event : MonoBehaviour
{
    [SerializeField] GameObject CharacterInfoPanel;

    void Update()
    {
    }

    public void PlayerButtonClick()
    {
        CharacterInfoPanel.SetActive(true);
    }

    public void BackCharInfoPanel()
    {
        CharacterInfoPanel.SetActive(false);
    }

    public void SelectAndBack()
    {
        CharacterInfoPanel.SetActive(false);
        SceneManager.LoadScene("MainScene");
    }
}
