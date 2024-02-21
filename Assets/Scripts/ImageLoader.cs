using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ImageLoader : MonoBehaviour
{
    public string url = "https://lh3.google.com/u/0/d/1qQj0V0HvbNxd5DxDheXU52kqyVKOP-SY";
    public Renderer thisRenderer;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadFromLikeCoroutine());
        thisRenderer.material.color = Color.white;
    }

    private IEnumerator LoadFromLikeCoroutine() {
        WWW wwwLoader = new WWW(url);
        yield return wwwLoader;
        thisRenderer.material.color = Color.white;
        thisRenderer.material.mainTexture = wwwLoader.texture; // set loaded image
    }
}
