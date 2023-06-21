using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public static class ImageService
{
    public static IEnumerator LoadImage(string url, Action<Texture> callback)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();


        if (request.result == UnityWebRequest.Result.ConnectionError 
            || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("Error fetching data");
            callback.Invoke(null);
        }
        else
        {
            callback.Invoke(((DownloadHandlerTexture)request.downloadHandler).texture);
        }
    }
}
