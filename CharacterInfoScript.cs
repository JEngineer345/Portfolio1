using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Firebase.Database;
using UnityEngine.UI;

public class CharacterInfoScript : MonoBehaviour
{
    [SerializeField] Text NameText;
    [SerializeField] Text GenderText;
    [SerializeField] Text ExplanationText;
    [SerializeField] GameObject CharacterInfoPanel;

    FirebaseDatabase database;
    DatabaseReference reference;
    PlayerManagerScript playerManager;

    UnityMainThreadDispatcher dispatcher;

    void Start()
    {
        database = FirebaseDatabase.GetInstance("https://defensegame-b4528-default-rtdb.firebaseio.com/");
        reference = database.RootReference;

        dispatcher = UnityMainThreadDispatcher.Instance();

        playerManager = FindObjectOfType<PlayerManagerScript>();

        CharacterInfoPanel.SetActive(false);
    }

    void Update()
    {
        InfoRun();
    }

    void InfoRun()
    {
        reference.Child("CharacterInfo").OrderByChild("KeyValue").EqualTo(playerManager.index).GetValueAsync().ContinueWith(task => 
        {
            if (task.IsFaulted)
            {
                Debug.Log("Data Loading Error");
            }

            else if (task.IsCompleted)
            {
                Debug.Log(playerManager.index);
                DataSnapshot snapshot = task.Result;
                foreach (DataSnapshot data in snapshot.Children)
                {
                    dispatcher.Enqueue(() =>
                    {
                        if (NameText != null)
                        {
                            NameText.GetComponent<Text>().text = (string)data.Child("Name").Value;
                        }
                        if (GenderText != null)
                        {
                            GenderText.GetComponent<Text>().text = (string)data.Child("Gender").Value;
                        }
                        if (ExplanationText != null)
                        {
                            ExplanationText.GetComponent<Text>().text = (string)data.Child("Explanation").Value;
                        }
                    });
                }
            }
        });
    }
}