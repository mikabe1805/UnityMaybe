using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class array : MonoBehaviour
{
    // this whole file might be useless, but ill keep it for now
    public int[] IDs;
    public int ready = 0;
    public string lastClicked;
    public int allOut = 0; // some way to keep track of paintings loading
    int maxID;
    JSONNode o;

    // Start is called before the first frame update
    // here's what I want to do: Instead of just 1 IDs list, maybe we can have multiple for each type??
    void Start()
    {
        // what's going to happen here is that it asks 
        // the read google sheet script for the ids
        //for (int)
        maxID = GameObject.FindWithTag("Scripts").GetComponent<ReadGoogleSheet>().maxID;
        print(maxID);
    }

    // Update is called once per frame
    void Update()
    {
        if (maxID == 0) {
            maxID = GameObject.FindWithTag("Scripts").GetComponent<ReadGoogleSheet>().maxID;
        }
        if (maxID != 0 && ready == 0) {
            ready = 1;
            print(maxID);
            makeList();
        }
    }

    void makeList() {
        IDs = new int[maxID];
        for (int i = 1; i <= maxID; i++) {
            IDs[i-1] = i;
        }
        int j = 0;
        o = GameObject.FindWithTag("Scripts").GetComponent<ReadGoogleSheet>().o;
        foreach (var item in o) {
            var itemo = JSON.Parse(item.ToString());
            if (itemo[0]["Valid"] == false) {
                IDs[j] = 0;
            }
            j++;
        }
    }
}
