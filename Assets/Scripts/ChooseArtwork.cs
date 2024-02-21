using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using TMPro;

public class Gallery
{
    public List<Piece> Artworks;
}

public class Piece
{
    public bool Valid;
    public string ID;
    public string ArtistName;
    public string ArtworkName;
    public string Description;
    public string MediaFormat;
    public string[] Tags;
    public string UploadArtwork;
    public Texture Art;
}

public class ChooseArtwork : MonoBehaviour {
    public GameObject contentList;
    public GameObject button;
    
    public void ListArtwork()
    {
        GameObject artButton;
        foreach (Piece p in SingletonGallery.Instance.g.Artworks)
        {
            artButton = Instantiate(button, contentList.transform);
            artButton.GetComponentInChildren<TextMeshProUGUI>().text = p.ArtworkName;
            artButton.GetComponent<ButtonArtwork>().artwork = p;
        }
        
    }
}

