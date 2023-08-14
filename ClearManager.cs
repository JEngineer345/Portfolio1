using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Firebase.Database;
using System;

public class ClearManager : MonoBehaviour
{
    FirebaseDatabase database;
    DatabaseReference reference;

    [SerializeField] GameObject Clearpanel;
    [SerializeField] Text GetCoinText;

    CoinManager coinManager;

    string DeviceID;

    void Start()
    {
        Clearpanel.SetActive(false);

        database = FirebaseDatabase.GetInstance("https://defensegame-b4528-default-rtdb.firebaseio.com/");
        reference = database.RootReference;

        coinManager = FindObjectOfType<CoinManager>();

        DeviceID = System.Environment.MachineName;
    }

    public void GameClearPanel(bool BossState)
    {
        if (BossState == false)
        {
            Clearpanel.SetActive(true);

            GetCoinText.text = "Å‰µæÇÑ Coin: " + coinManager.CoinCount;

            reference.Child("User").Child("UserInfo").OrderByChild("DeviceID").GetValueAsync().ContinueWith(task =>
            {
                if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    foreach (DataSnapshot data in snapshot.Children)
                    {
                        if (DeviceID == (string)data.Child("DeviceID").Value)
                        {
                            Debug.Log((string)data.Child("UserNickName").Value);
                            var coinData = new Dictionary<string, object>
                            {
                                 { "Coin", coinManager.CoinCount }
                            };

                            int CurrentCoin= Convert.ToInt32(data.Child("Coin").Value);
                            coinData["Coin"] = coinManager.CoinCount + CurrentCoin;

                            data.Reference.UpdateChildrenAsync(coinData).ContinueWith(coinTask =>
                            {
                                if (coinTask.IsCompleted)
                                {
                                    Debug.Log("Coin Data Update!");
                                }
                                else if (coinTask.IsFaulted)
                                {
                                    Debug.Log("Coin Data Error");
                                }
                            });
                        }
                    }
                }

                else if (task.IsFaulted)
                {
                    Debug.Log("Loading Error");
                }
            });
        }
    }
}
