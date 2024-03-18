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
public struct Data3 {
   public string Name ;
   public string ImageURL ;
}
public class Demo3 : MonoBehaviour {
//    [SerializeField] Text uiNameText ;
//    [SerializeField] RawImage uiRawImage ;
    // [SerializeField] Image placeImageOnLoad;
    // [SerializeField] RectTransform imageRect;
    // [SerializeField] int desiredWidth = 400;
    // [SerializeField] int desiredHeight = 400;
   public bool allGood = false; 
   public GameObject PopUp;

   public Texture tex;

   public bool special = false;

   void Start () {
   }

   void Update() {
      bool ready = PopUp.GetComponent<popupManager>().done;
      if (ready && !allGood) {
         allGood = true;
         tex = PopUp.GetComponent<popupManager>().tex;
         DownloadImage();
      }
   }

   void DownloadImage()
{   
        Texture2D texture = (Texture2D) tex;
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
         if (special) {
            GameObject.Find("ZoomFather").GetComponent<Resize>().resize();
         }
      //   UpdateRect();
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