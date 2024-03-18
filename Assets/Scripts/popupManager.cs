using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class popupManager : MonoBehaviour
{
    string target;
    public TextMeshProUGUI artwork;
    public TextMeshProUGUI artist;
    public TextMeshProUGUI description;
    public TextMeshProUGUI tags;

    public Image image;
    public Image image2;
    public string URL;
    public Texture tex;
    public bool done = false;
    GameObject main;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.activeSelf && !done) {
            prepare();
            done = true;
        }
    }

    void prepare() {
        target = GameObject.FindWithTag("ArrayManager").GetComponent<array>().lastClicked;
        print(target);
        main = GameObject.Find(target);
        Debug.Log(GameObject.Find(target));
        URL = main.GetComponent<ImageManager>().URL;
        tex = main.GetComponent<ImageManager>().tex;
        artist.text = main.GetComponent<ImageManager>().Artist_Name;
        artwork.text = main.GetComponent<ImageManager>().Artwork_Name;
        description.text = main.GetComponent<ImageManager>().description;
        tags.text = main.GetComponent<ImageManager>().tags;
        image.GetComponent<Demo3>().allGood = false;
        image2.GetComponent<Demo3>().allGood = false;
    }
}
