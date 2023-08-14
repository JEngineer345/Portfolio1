using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

using Firebase;
using Firebase.Extensions;
using Firebase.Storage;
using UnityEngine.UI;

using System.Threading.Tasks;
using System.IO;
using System;

public class FirebaseScript : MonoBehaviour
{
    [SerializeField] RawImage loadImage;
    FirebaseStorage storage;
    StorageReference reference;

    void Start()
    {
        storage = FirebaseStorage.DefaultInstance;
        reference = storage.GetReferenceFromUrl("gs://defensegame-b4528.appspot.com/LibImage");

        StorageReference storageReference = reference.Child("Farmer11.png");

        storageReference.GetDownloadUrlAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted == false && task.IsCanceled == false)
            {
                StartCoroutine(imageLoad(Convert.ToString(task.Result)));
            }
        });
    }

    void Update()
    {
    }

    IEnumerator imageLoad(string MediaUrl)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(request.error);
        }

        else
        {
            loadImage.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
        }
    }
}
