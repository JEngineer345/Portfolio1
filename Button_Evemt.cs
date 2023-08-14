using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Firebase.Database;

public class Button_Evemt : MonoBehaviour
{
    FirebaseDatabase database;
    DatabaseReference reference;

    AudioSource ClickSource;

    string DeviceID;

    private UnityMainThreadDispatcher dispatcher;

    void Start()
    {
        ClickSource = GetComponent<AudioSource>();

        dispatcher = UnityMainThreadDispatcher.Instance();

        database = FirebaseDatabase.GetInstance("https://defensegame-b4528-default-rtdb.firebaseio.com/");
        reference = database.RootReference;

        DeviceID = System.Environment.MachineName;
    }

    void Update()
    {
    }

    public void GoToScene()
    {
        ClickSource.Play();
        reference.Child("User").Child("UserInfo").OrderByChild("DeviceID").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("Error");
            }

            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                bool isRegistered = false;
                foreach (DataSnapshot data in snapshot.Children)
                {
                    string DeviceIDFromDB = (string)data.Child("DeviceID").Value;
                    if (DeviceIDFromDB == DeviceID)
                    {
                        isRegistered = true; // 기기의 고유 ID가 등록되어 있다면 true로 설정
                        break; // 반복문 종료
                    }
                }

                dispatcher.Enqueue(() =>
                {
                    if (isRegistered == true)
                    {
                        SceneManager.LoadScene("MainScene");
                    }

                    else
                    {
                        SceneManager.LoadScene("RegisterScene");
                    }
                });
            }
        });
    }
    public void GoToShopScene()
    {
        ClickSource.Play();
        SceneManager.LoadScene("ShopScene");
    }

    public void GoToSettingScene()
    {
        ClickSource.Play();

    }

    public void GameToMain()
    {
        ClickSource.Play();
        SceneManager.LoadScene("MainScene");
    }

    public void GoToBack()
    {
        ClickSource.Play();
        SceneManager.LoadScene("MainScene");
    }

    public void GoToPlayerManager()
    {
        ClickSource.Play();
        SceneManager.LoadScene("PlayerManagerScene");
    }

    public void GoToGround()
    {
        ClickSource.Play();
        SceneManager.LoadScene("SampleScene");
    }

    public void GameQuit()
    {
        Application.Quit();
    }
}
