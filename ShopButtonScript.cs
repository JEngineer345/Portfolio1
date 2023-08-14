using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using Firebase.Database;

public class ShopButtonScript : MonoBehaviour
{
    [SerializeField] GameObject ShopPanel;
    [SerializeField] Text TitleText;

    public int KeyValue;

    FirebaseDatabase database;
    DatabaseReference reference;

    UnityMainThreadDispatcher dispatcher;

    void Start()
    {
        ShopPanel.SetActive(false);

        database = FirebaseDatabase.GetInstance("https://defensegame-b4528-default-rtdb.firebaseio.com/");
        reference = database.RootReference;

        dispatcher = UnityMainThreadDispatcher.Instance();
    }

    void Update()
    {
    }

    public void ClickedButton()
    {
        ShopPanel.SetActive(true);

        reference.Child("ShopInfo").OrderByChild("KeyValue").EqualTo(KeyValue).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("Data Load Error");
            }

            else if (task.IsCompleted)
            {
                DataSnapshot data = task.Result;
                foreach (DataSnapshot BoxData in data.Children)
                {
                    dispatcher.Enqueue(() =>
                    {
                        TitleText.text = (string)BoxData.Child("Name").Value;
                    });
                }
            }
        });
    }

    public void BackClick()
    {
        ShopPanel.SetActive(false);
    }
}
