using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System;

using Firebase.Database;

using Newtonsoft.Json;

public class PlayerManagerScript : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] Image image;
    [SerializeField] Text NameText;
    [SerializeField] Text GenderText;
    [SerializeField] Text ExplanationText;
    [SerializeField] GameObject CharacterInfoPanel;

    public int index;

    FirebaseDatabase database;
    DatabaseReference reference;

    UnityMainThreadDispatcher dispatcher;

    string DeviceID;

    void Start()
    {
        database = FirebaseDatabase.GetInstance("https://defensegame-b4528-default-rtdb.firebaseio.com/");
        reference = database.RootReference;

        DeviceID = System.Environment.MachineName;
        dispatcher = UnityMainThreadDispatcher.Instance();

        CharacterInfoPanel.SetActive(false);
    }

    void Update()
    {
        ButtonBoolFromServer();
    }

    void ButtonBoolFromServer()
    {
        reference.Child("User").Child("UserInfo").OrderByChild("DeviceID").GetValueAsync().ContinueWith(loadingTask =>
        {
            if (loadingTask.IsFaulted)
            {
                Debug.Log("Data Loading Error");
            }

            else if (loadingTask.IsCompleted)
            {
                //Debug.Log(loadingTask.Result);
                DataSnapshot snapshot = loadingTask.Result;
                foreach (DataSnapshot data in snapshot.Children)
                {
                    if (DeviceID == (string)data.Child("DeviceID").Value)
                    {
                        string characterList = (string)data.Child("CharacterList").Value;
                        Dictionary<string, bool> charDic = JsonToDictionary(characterList);

                        if (charDic.TryGetValue("Char" + index, out bool CharKeyValue))
                        {
                            dispatcher.Enqueue(() =>
                            {
                                if (CharKeyValue == false)
                                {
                                    button.interactable = false;
                                    image.color = Color.gray;
                                }

                                else if (CharKeyValue == true)
                                {
                                    button.interactable = true;
                                }
                            });
                        }                 
                    }
                }
            }
        });
    }

    public void InfoRun()
    {
        reference.Child("CharacterInfo").OrderByChild("KeyValue").EqualTo(index).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("Data Loading Error");
            }

            else if (task.IsCompleted)
            {
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

    private Dictionary<string, bool> JsonToDictionary(string json)
    {
        return JsonConvert.DeserializeObject<Dictionary<string, bool>>(json);
    }
}