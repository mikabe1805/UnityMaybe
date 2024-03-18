using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class ImageManager : MonoBehaviour
{
    // Start is called before the first frame update
    public string URL;
    public GameObject PopUp;
    public int readyyy = 0;
    public string Media;
    public int ID;
    public string Artist_Name;
    public string Artwork_Name;
    public string tags;
    public string description;
    public Texture tex;
    string Media_Format;
    string Class;
    int ready;
    int done = 0; // can be made into boolean
    // check for if the image is valid. if not, just skip it and take the id out anyways 
    bool allDataGathered = false;

    JSONNode o;
    int[] IDs;
    void Start()
    {
        Media = GetComponentInParent<mediaManager>().mediaType;
        // PopUp = GameObject.Find("pop up");
        // o = GameObject.FindWithTag("Scripts").GetComponent<ReadGoogleSheet>().o;
        // int IDs = 
    }

    // Update is called once per frame
    void Update()
    {
        ready = GameObject.FindWithTag("ArrayManager").GetComponent<array>().ready;
        if (ready == 1 && done == 0) {
            done = 1;
            getReady();
        }
        if (!allDataGathered) {
            if (GetComponent<Demo>().complete) {
                allDataGathered = true;
                tex = GetComponent<Demo>().tex;
            }
        }
    }

    void getReady() {
        // print("things are looking good so far");
        o = GameObject.FindWithTag("Scripts").GetComponent<ReadGoogleSheet>().o;
        IDs = GameObject.FindWithTag("ArrayManager").GetComponent<array>().IDs;
        // foreach (int id in IDs) {
        //     if (id != 0) {

        //     }
        // }
        int i = 0;
        LoopThrough(i);
        foreach (var item in o) {
                var itemo = JSON.Parse(item.ToString());
                var iii = itemo[0]["ID"];
                if (iii == ID) {
                    if(!(itemo[0]["Media_Format"].Equals(Media)) && iii != 69) {
                        // print(itemo[0]["Media_Format"]);
                        LoopThrough(ID); // id is equal to i + 1, so we're continuing forward
                        continue;
                    }
                    Artist_Name = itemo[0]["Artist_Name"];
                    Artwork_Name = itemo[0]["Artwork_Name"];
                    URL = itemo[0]["Upload_Artwork"];
                    description = itemo[0]["Description"];
                    tags = itemo[0]["Tags"];
                    GameObject.FindWithTag("ArrayManager").GetComponent<array>().IDs[ID-1] = 0;
                    break;
                }
            }
            // print(URL);
            readyyy = 1;
    }

// !string.Equals(JSON.Parse(o[i].ToString())[0]["Media_Format"], Media
    void LoopThrough(int i) {
        if (i==IDs.Length-1) { // this condition means that there are no more available of a certain type
            // set it defective when the chance arises
            // MIKA!!!!!!!!!!!!! JUST SEND A COMMAND TO DISABLE ITSELF
            print("okay?");
            ID = 69;
            this.gameObject.SetActive(false);
        } else {
            while (i < IDs.Length && IDs[i] == 0) {
                i++;
            }
            ID = IDs[i];
        }
    }
    void OnMouseDown()
    { // get back to this to disable the click option for invalid artworks
        if (Cursor.lockState == CursorLockMode.Locked) {
            PopUp.SetActive(true);
            PopUp.GetComponent<popupManager>().done = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            GameObject.Find("PlayerCapsule").GetComponent<StarterAssets.StarterAssetsInputs>().cursorInputForLook = false;// maybe a code breaker?
            GameObject.FindWithTag("ArrayManager").GetComponent<array>().lastClicked = this.gameObject.name;
            print(this.gameObject.name);
        }
    }
}
