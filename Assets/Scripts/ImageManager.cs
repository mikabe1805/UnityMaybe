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
    public int readyyy = 0;
    public string Media;
    public int ID;
    public string Artist_Name;
    public string Artwork_Name;
    string Media_Format;
    string Class;
    int ready;
    int done = 0; // can be made into boolean
    // check for if the image is valid. if not, just skip it and take the id out anyways 

    JSONNode o;
    int[] IDs;
    void Start()
    {
        Media = GetComponentInParent<mediaManager>().mediaType;
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
                    if(!(itemo[0]["Media_Format"].Equals(Media))) {
                        print(itemo[0]["Media_Format"]);
                        LoopThrough(ID); // id is equal to i + 1, so we're continuing forward
                        continue;
                    }
                    Artist_Name = itemo[0]["Artist_Name"];
                    Artwork_Name = itemo[0]["Artwork_Name"];
                    URL = itemo[0]["Upload_Artwork"];
                    GameObject.FindWithTag("ArrayManager").GetComponent<array>().IDs[ID-1] = 0;
                    break;
                }
            }
            print(URL);
            readyyy = 1;
    }

// !string.Equals(JSON.Parse(o[i].ToString())[0]["Media_Format"], Media
    void LoopThrough(int i) {
        while (i < IDs.Length && IDs[i] == 0) {
            i++;
        }
        ID = IDs[i];
        // GameObject.FindWithTag("ArrayManager").GetComponent<array>().IDs[i] = 0;
    }
}
