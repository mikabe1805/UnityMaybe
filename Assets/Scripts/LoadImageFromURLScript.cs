using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LoadImageFromURLScript : MonoBehaviour
{
    #region Image On Load Variables
    [Header("Image On Load Variables")]
    [SerializeField] Image placeImageOnLoad;
    [SerializeField] RectTransform imageRect;
    [SerializeField] int desiredWidth = 400;
    [SerializeField] int desiredHeight = 400;
    #endregion

    // [Header("Input field to retrieve Image URL")]
    // [SerializeField] TMP_InputField urlInputField;

    // [Header("The image to show loading progress")]
    // [SerializeField] Image progressBarImage;

    // Start is called before the first frame update

    public string url = "https://static.vecteezy.com/system/resources/previews/009/210/264/non_2x/dinosaur-character-cartoon-cute-kawaii-animal-illustration-clipart-free-vector.jpg";
    void Start()
    {
        //desiredWidth = Screen.width/2;
        InputFieldEditEnd();
    }

    public void InputFieldEditEnd(){
        // string url = urlInputField.text;

        print(url);

        if(!string.IsNullOrWhiteSpace(url)){
            StartCoroutine(LoadImageFromURL(url));
        }
    }

    ///<summary>
    /// A function that takes in a string (url) and tries to set an image variable with the result
    ///</summary>
    IEnumerator LoadImageFromURL(string url){
        // The basic web request
        UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(url);

        // The Async Operation variable, which is needed to view download progress
        UnityWebRequestAsyncOperation request = uwr.SendWebRequest();

        //Turn on the progress bar image
        // progressBarImage.gameObject.SetActive(true);

        //Update the progress bar while request is loading
        while(!request.isDone)
        {
            // progressBarImage.fillAmount = request.progress;
            yield return null;
        }

        //Image has finished loading, turn off progress bar
        // progressBarImage.gameObject.SetActive(false);

        //Check for success
        if(uwr.result != UnityWebRequest.Result.Success){
            Debug.LogError(uwr.error);
            ImageFinishedLoading(false);
        }
        else{
            //Create a texture2D from UnityWebRequest
            Texture2D texture = ((DownloadHandlerTexture)uwr.downloadHandler).texture;

            //Sprite display variables
            float width = texture.width,
                height = texture.height,
                widthSub = 1,
                heightSub = 1;

            //Create a sprite from the texture
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, width, height), Vector2.zero);

            #region Sprite display variable creation area
            //is the width greater than the height?
            //These methods ensure that our image ratio does not change
            if(width > height){
                widthSub = width/height;
            }
            else if(height > width){
                heightSub = height/width;
            }

            //Reduce the image width and height until it fits inside of our desired width and height
            while(width > desiredWidth || height > desiredHeight){
                width -= widthSub;
                height -= heightSub;
            }
            #endregion

            //Set the image size
            imageRect.sizeDelta = new Vector2(width, height);

            //Assign the sprite to the image so it can be viewed on the canves
            placeImageOnLoad.sprite = sprite;

            ImageFinishedLoading(true);
        }
    }

    ///<param name="success">If the image has loaded successfully or not</param>
    void ImageFinishedLoading(bool success){
        if(success){
            print("Image loaded successfully");
        }else{
            print("There was an error loading the image, see console above for more details.");
        }
    }
}