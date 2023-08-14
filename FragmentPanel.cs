using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FragmentPanel : MonoBehaviour
{
    [SerializeField] GameObject panel;

    //FirebaseStorage firebaseStorage = FirebaseStorage.DefaultInstance;
    //StorageReference reference;

    //Task<Uri> task;

    void Start()
    {
        panel.SetActive(false);
        //reference = firebaseStorage.GetReference("ShopCharacter/Detective1.PNG");
    }

    void Update()
    {
        //reference.GetDownloadUrlAsync().ContinueWith(task => {
        //    if (!task.IsFaulted && !task.IsCanceled)
        //    {
        //        Debug.Log("�̹��� �ε� ����!");
        //    }
        //});
    }

    public void GoToPanel()
    {
        panel.SetActive(true);
    }

    public void GoToBackShopPage()
    {
        panel.SetActive(false);
    }
}
