using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Firebase;
using Firebase.Database;

public class DatabaseScript : MonoBehaviour
{
    FirebaseDatabase database;
    DatabaseReference reference;

    [SerializeField] Text OverlapText;
    [SerializeField] InputField input_nick;

    [SerializeField] Toggle Mantoggle;
    [SerializeField] Toggle Womantoggle;

    string UserNickName;
    string UserGender;
    int UserLevel = 1;
    int Exp = 0;
    string DeviceID;
    int Coin = 1000;
    int Ruby = 50;
    Dictionary<string, bool> CharacterList = new Dictionary<string, bool>();

    private UnityMainThreadDispatcher dispatcher;

    void Start()
    {
        database = FirebaseDatabase.GetInstance("https://defensegame-b4528-default-rtdb.firebaseio.com/");
        reference = database.RootReference;

        dispatcher = UnityMainThreadDispatcher.Instance();
        DeviceID = System.Environment.MachineName;

        CharacterList.Add("Char1", true);
        CharacterList.Add("Char2", false);
        CharacterList.Add("Char3", false);
        CharacterList.Add("Char4", false);
        CharacterList.Add("Char5", false);
        CharacterList.Add("Char6", false);
        CharacterList.Add("Char7", false);
        CharacterList.Add("Char8", false);
        CharacterList.Add("Char9", false);
    }

    void Update()
    {
    }

    public void GetClickButton()
    {
        UserNickName = input_nick.text;
        OnClickCheck();

        reference.Child("User").Child("UserInfo").OrderByChild("UserNickName").EqualTo(UserNickName).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("Error");
            }

            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                if (snapshot.HasChildren)
                {
                    dispatcher.Enqueue(() => {
                        OverlapText.text = "중복된 닉네임입니다.";
                    });
                }

                else
                {
                    string CharacterListJson = DictionaryToJson(CharacterList);

                    User user = new User(UserNickName, UserGender, DeviceID, Exp, UserLevel, Coin, Ruby, CharacterListJson);
                    string json = JsonUtility.ToJson(user);

                    reference.Child("User").Child("UserInfo").Push().SetRawJsonValueAsync(json).ContinueWith(uploadTask =>
                    {
                        if (uploadTask.IsCompleted)
                        {
                            dispatcher.Enqueue(() =>
                            {
                                SceneManager.LoadScene("MainScene");
                            });
                        }
                        else if (uploadTask.IsFaulted)
                        {
                            Debug.Log("데이터 업로드 실패");
                        }
                    });
                }
            }
        });
    }

    private void OnClickCheck()
    {
        if (Mantoggle.isOn == true)
        {
            UserGender = "Man";
        }

        else if (Womantoggle.isOn == true)
        {
            UserGender = "Woman";
        }
    }

    private string DictionaryToJson(Dictionary<string, bool> dictionary)
    {
        return "{" + string.Join(",", dictionary.Select(kv => "\"" + kv.Key + "\":" + kv.Value.ToString().ToLower())) + "}";
    }
}

public class User
{
    public string UserNickName;
    public string Gender;
    public int UserLevel;
    public int Exp;
    public string DeviceID;
    public int Coin;
    public int Ruby;
    public string CharacterList;

    public User() {}

    public User(string UserNickName, string Gender, string DeviceID, int Exp, int UserLevel, int Coin, int Ruby, string CharacterListJson)
    {
        this.UserNickName = UserNickName;
        this.Gender = Gender;
        this.UserLevel = UserLevel;
        this.Exp = Exp;
        this.DeviceID = DeviceID;
        this.Coin = Coin;
        this.Ruby = Ruby;
        CharacterList = CharacterListJson;

    }
}