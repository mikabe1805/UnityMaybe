using UnityEngine ;
using UnityEngine.Networking ;
using UnityEngine.UI ;
using System.Collections ;

// Json data format
/*
      {
        "Name"     : "..." ,
        "ImageURL" : "..."
      }
*/
public struct Data {
   public string Name ;
   public string ImageURL ;
}
public class Demo : MonoBehaviour {
//    [SerializeField] Text uiNameText ;
//    [SerializeField] RawImage uiRawImage ;
    // [SerializeField] Image placeImageOnLoad;
    // [SerializeField] RectTransform imageRect;
    // [SerializeField] int desiredWidth = 400;
    // [SerializeField] int desiredHeight = 400;
    public string URL = "";

    public Texture tex;

    public bool special = false;

    public bool complete = false;
    
   int allGood = 0; 

   void Start () {
      //   string baseUrl="https://drive.google.com/uc?export=download&id";
      //   string imageId = URL.Substring(32); //this will extract the image ID from the shared image link
      //   string jsonURL = baseUrl + imageId;
      // StartCoroutine(DownloadImage(jsonURL));
   }

   void Update() {
      int ready = GetComponent<ImageManager>().readyyy;
      if (ready == 1 && allGood == 0) {
         allGood = 1;
         URL = GetComponent<ImageManager>().URL;
         string baseUrl="https://drive.google.com/uc?export=download&id";
         string imageId = URL.Substring(32); //this will extract the image ID from the shared image link
         string jsonURL = baseUrl + imageId;
         StartCoroutine(DownloadImage(jsonURL)); // make it so that this calls on updaterect
         // StartCoroutine(UpdateRect());
      }
   }
   void UpdateRect() {
         // yield return new WaitForSeconds(8);
         MeshRenderer mr = this.gameObject.GetComponent<MeshRenderer>();
         tex = mr.material.mainTexture;
         float x;
         float y;
         float g;
         if (special) {
            g = 80;
         } else {
            g = 2;
         }
         if (tex.height < tex.width) {
            float ratio = (float)tex.height / tex.width;
            // print("ratio: " + ratio);
            x = g; //width
            y = x * ratio;
         } else {
            float ratio = (float)tex.width / tex.height;
            // print("ratio: " + ratio);
            y = g; //height
            x = y * ratio;
         }
         gameObject.transform.localScale = new Vector3(x, y, 1);
         complete = true;
         GameObject.FindWithTag("ArrayManager").GetComponent<array>().allOut += 1;
   }

   IEnumerator DownloadImage(string MediaUrl)
{   
   // this is the one that gets the texture to change to the image online
    UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
    yield return request.SendWebRequest();
    if(request.isNetworkError || request.isHttpError) 
        Debug.Log(request.error);
    else
        this.gameObject.GetComponent<Renderer>().material.mainTexture = ((DownloadHandlerTexture) request.downloadHandler).texture;
        UpdateRect();
} 

   IEnumerator GetData (string url) {
      UnityWebRequest request = UnityWebRequest.Get (url) ;

      yield return request.SendWebRequest() ;

      if (request.isNetworkError || request.isHttpError) {
         // error ...

      } else {
         // success...
         Data data = JsonUtility.FromJson<Data> (request.downloadHandler.text) ;

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