using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonArtwork : MonoBehaviour
{
    public GameObject view;
    public Piece artwork;

    private void Start()
    {
        view = GameObject.Find("ArtworkView");
    }

    public void StartFetch()
    {
        StartCoroutine(FetchArtwork());
    }
    private IEnumerator FetchArtwork()
    {
        if (artwork.Art == null)
        {
            yield return StartCoroutine(SingletonGallery.Instance.GetRequest(artwork));
        }
        view.GetComponent<RawImage>().texture = artwork.Art;

    }
}
