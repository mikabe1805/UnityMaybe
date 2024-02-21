using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

public class SingletonGallery : MonoBehaviour
{
    public Gallery g;

    private string _Credentials;
    public static SingletonGallery Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
        StartCoroutine(GetCredentials());

    }

    public IEnumerator GetRequest(Piece artwork)
    {
        string uri = artwork.UploadArtwork.Replace("open?", "uc?export=download&");
        /*uri = string.Concat(uri, "export=download");
        if (!uri.StartsWith("https"))
        {
            uri = string.Concat("https://drive.google.com/", uri);
        }*/
        Debug.Log(uri);
        UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(uri);
        // Request and wait for the desired page.
        webRequest.SendWebRequest();
        yield return new WaitUntil(() => webRequest.downloadHandler.isDone);
        

        string[] pages = uri.Split('/');
        int page = pages.Length - 1;
        switch (webRequest.result)
        {
            case UnityWebRequest.Result.ConnectionError:
            case UnityWebRequest.Result.DataProcessingError:
                Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                Debug.Log(webRequest.downloadHandler.error);
                break;
            case UnityWebRequest.Result.ProtocolError:
                Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                break;
            case UnityWebRequest.Result.Success:
                Texture2D t = ((DownloadHandlerTexture)webRequest.downloadHandler).texture;
                Debug.Log(t.height);

                artwork.Art = t;
                break;
        }
    }

    public IEnumerator GetCredentials()
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(Path.Combine(Application.streamingAssetsPath, "credentials.json"));
        webRequest.SendWebRequest();
        yield return new WaitUntil(() => webRequest.downloadHandler.isDone);
        _Credentials = webRequest.downloadHandler.text;
        g = AccessGallery.ConnectGSheets.Main();
        GetComponent<ChooseArtwork>().ListArtwork();
    }

    public string GetJSONCredentials()
    {
        return _Credentials;
    }
}
