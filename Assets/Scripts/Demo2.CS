using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

// Json data format
/*
      {
        "Name"     : "..." ,
        "ImageURL" : "..."
      }
*/
public struct Data2 {
   public string Name ;
   public string ImageURL ;
}
public class Demo2 : MonoBehaviour {
//    [SerializeField] Text uiNameText ;
//    [SerializeField] RawImage uiRawImage ;
    // [SerializeField] Image placeImageOnLoad;
    // [SerializeField] RectTransform imageRect;
    // [SerializeField] int desiredWidth = 400;
    // [SerializeField] int desiredHeight = 400;
   public string URL = "";
   public bool allGood = false; 
   public GameObject PopUp;

   void Start () {
      //   string baseUrl="https://drive.google.com/uc?export=download&id";
      //   string imageId = URL.Substring(32); //this will extract the image ID from the shared image link
      //   string jsonURL = baseUrl + imageId;
      // StartCoroutine(DownloadImage(jsonURL));
   }

   void Update() {
      bool ready = PopUp.GetComponent<popupManager>().done;
      if (ready && !allGood) {
         allGood = true;
         URL = PopUp.GetComponent<popupManager>().URL;
         string baseUrl="https://drive.google.com/uc?export=download&id";
         string imageId = URL.Substring(32); //this will extract the image ID from the shared image link
         string jsonURL = baseUrl + imageId;
         StartCoroutine(DownloadImage(jsonURL)); // make it so that this calls on updaterect
         // StartCoroutine(UpdateRect());
      }
   }
   void UpdateRect() {
         // yield return new WaitForSeconds(8);
         MeshRenderer mr = gameObject.GetComponent<MeshRenderer>();
         Texture tex = mr.material.mainTexture;
         float x;
         float y;
         if (tex.height < tex.width) {
            float ratio = (float)tex.height / tex.width;
            // print("ratio: " + ratio);
            x = 2; //width
            y = x * ratio;
         } else {
            float ratio = (float)tex.width / tex.height;
            // print("ratio: " + ratio);
            y = 2; //height
            x = y * ratio;
         }
         gameObject.transform.localScale = new Vector3(x, y, 1);
   }

   IEnumerator DownloadImage(string MediaUrl)
{   
   // this is the one that gets the texture to change to the image online
    UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
    yield return request.SendWebRequest();
    if(request.isNetworkError || request.isHttpError) 
        Debug.Log(request.error);
    else {
        Texture2D texture = ((DownloadHandlerTexture) request.downloadHandler).texture;
        Sprite sprite = Sprite.Create(texture, new Rect(0,0, texture.width, texture.height), Vector2.zero);

      //   gameObject.placeImageOnLoad.sprite = sprite;
        GetComponent<Image>().sprite = sprite;

         float x;
         float y;
         if (texture.height < texture.width) {
            float ratio = (float)texture.height / texture.width;
            // print("ratio: " + ratio);
            x = 2; //width
            y = x * ratio;
         } else {
            float ratio = (float)texture.width / texture.height;
            // print("ratio: " + ratio);
            y = 2; //height
            x = y * ratio;
         }
         gameObject.transform.localScale = new Vector2(x, y);
      //   UpdateRect();
    }
} 

   IEnumerator GetData (string url) {
      UnityWebRequest request = UnityWebRequest.Get (url) ;

      yield return request.SendWebRequest() ;

      if (request.isNetworkError || request.isHttpError) {
         // error ...

      } else {
         // success...
         Data2 data = JsonUtility.FromJson<Data2> (request.downloadHandler.text) ; // <data> might be a problem

         // print data in UI
        //  uiNameText.text = data.Name ;

         // Load image:
         StartCoroutine (GetImage (data.ImageURL)) ;
      }
      
      // Clean up any resources it is using.
      request.Dispose () ;
   }

   IEnumerator GetImage (string url) {
      UnityWebRequest request = UnityWebRequestTexture.GetTexture (url) ;

      yield return request.SendWebRequest() ;

      if (request.isNetworkError || request.isHttpError) {
         // error ...

      } else {
         //success...
        //  uiRawImage.texture = ((DownloadHandlerTexture)request.downloadHandler).texture ;
        // makes the texture into the image from online
        this.gameObject.GetComponent<Renderer>().material.mainTexture = ((DownloadHandlerTexture)request.downloadHandler).texture;
      }

      // Clean up any resources it is using.
      request.Dispose () ;
   }

}