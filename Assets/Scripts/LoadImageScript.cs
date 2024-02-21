using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LoadImageScript : MonoBehaviour
{
    public static LoadImageScript instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(this);
            }
        }
    }

    public void LoadImageFromURLWithCallback(UnityAction<Sprite> callback, string url)
    {
        StartCoroutine(LoadImageFromURL());

        IEnumerator LoadImageFromURL()
        {
            // The basic web request
            UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(url);

            // The Async Operation variable, which is needed to view download progress
            UnityWebRequestAsyncOperation request = uwr.SendWebRequest();

            //Update the progress bar while request is loading
            while (!request.isDone)
            {
                yield return null;
            }

            //Check for success
            if (uwr.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(uwr.error);
                callback(null);
            }
            else
            {
                //Create a texture2D from UnityWebRequest
                Texture2D texture = ((DownloadHandlerTexture)uwr.downloadHandler).texture;

                //Create a sprite from the texture
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);


                //Assign the sprite to the image so it can be viewed on the canves
                callback(sprite);
            }
        }
    }

    public void LoadImageFromURLWithCallbackAndProgressBar(UnityAction<Sprite> callback, string url, Image progressBarImage)
    {
        StartCoroutine(LoadImageFromURL());

        IEnumerator LoadImageFromURL()
        {
            // The basic web request
            UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(url);

            // The Async Operation variable, which is needed to view download progress
            UnityWebRequestAsyncOperation request = uwr.SendWebRequest();

            //Turn on the progress bar image
            progressBarImage.gameObject.SetActive(true);

            //Update the progress bar while request is loading
            while (!request.isDone)
            {
                progressBarImage.fillAmount = request.progress;
                yield return null;
            }

            //Image has finished loading, turn off progress bar
            progressBarImage.gameObject.SetActive(false);

            //Check for success
            if (uwr.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(uwr.error);
                callback(null);
            }
            else
            {
                //Create a texture2D from UnityWebRequest
                Texture2D texture = ((DownloadHandlerTexture)uwr.downloadHandler).texture;

                //Create a sprite from the texture
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);


                //Assign the sprite to the image so it can be viewed on the canves
                callback(sprite);
            }
        }
    }

    public Vector2 GetSizeDelta(int maxHeight, int maxWidth, Sprite sprite)
    {
        //Sprite display variables
        float width = sprite.texture.width,
            height = sprite.texture.height,
            widthSub = 1,
            heightSub = 1;

        #region Sprite display variable creation area
        //is the width greater than the height?
        //These methods ensure that our image ratio does not change
        if (width > height)
        {
            widthSub = width / height;
        }
        else if (height > width)
        {
            heightSub = height / width;
        }

        //Reduce the image width and height until it fits inside of our desired width and height
        while (width > maxWidth || height > maxHeight)
        {
            width -= widthSub;
            height -= heightSub;
        }
        #endregion

        //Set the image size
        return new Vector2(width, height);
    }
}