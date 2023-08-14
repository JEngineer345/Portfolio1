using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;

using Firebase;
using Firebase.Database;

public class UserInformationScript : MonoBehaviour
{
    FirebaseDatabase database;
    DatabaseReference reference;

    [SerializeField] Text UserID;

    string DeviceID;

    private UnityMainThreadDispatcher dispatcher;

    void Start()
    {
        database = FirebaseDatabase.GetInstance("https://defensegame-b4528-default-rtdb.firebaseio.com/");
        reference = database.RootReference;

        dispatcher = UnityMainThreadDispatcher.Instance();

        DeviceID = System.Environment.MachineName;
    }

    void Update()
    {
        reference.Child("User").Child("UserInfo").OrderByChild("DeviceID").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("Error");
            }

            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                foreach (DataSnapshot data in snapshot.Children)
                {
                    if ((string)data.Child("DeviceID").Value == DeviceID)
                    {
                        dispatcher.Enqueue(() =>
                        {
                            string DeviceIDText = (string)data.Child("DeviceID").Value;
                            UserID.text = (string)data.Child("UserNickName").Value;
                        });
                    }
                }
            }
        });
    }
}