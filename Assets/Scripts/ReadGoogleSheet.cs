using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class ReadGoogleSheet : MonoBehaviour
{
    // Start is called before the first frame update
    // public TextMeshProUGUI outputArea; // this is what controls the output. we can link this up to object probably.
    public int maxID;
    public JSONNode o;
    // public List<JSONNode> drawingList = new List<JSONNode>();
    // public List<JSONNode> paintingList = new List<JSONNode>();
    // public List<JSONNode> digitalImageList = new List<JSONNode>();
    // public List<JSONNode> photographyList = new List<JSONNode>();
    // public List<JSONNode> paperList = new List<JSONNode>();
    // public List<JSONNode> sculptureList = new List<JSONNode>();
    // public List<JSONNode> charcoalList = new List<JSONNode>();
    void Start()
    {
        StartCoroutine(ObtainSheetData());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ObtainSheetData()
    {
        UnityWebRequest www = UnityWebRequest.Get("https://script.googleusercontent.com/macros/echo?user_content_key=6nGL0nD-hMZCwKxi0WGt9cT-SbmLXzDm_weJcZ8XFkHHMzwnVq1eE_toCwAPEuwPOYV4nddWqnrYNCLO1l9MbxFdhefE1eI0m5_BxDlH2jW0nuo2oDemN9CCS2h10ox_1xSncGQajx_ryfhECjZEnCnLQSEnXjF0n2VOP3lZG2zBa3QusEnKQgfx2JfMaCTZC6JmiuF0ny-b4HXlXq8kiirf5n2Bsl3f_3AxEnF_ZZRFmNp2exz8OQ&lib=MapPpBtNttTVeU4fydvpN6JhG3lFpo0w5");
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError) {
            Debug.Log("ERROR: " + www.error);
        }
        else {
            // string updateText="";
            string json = www.downloadHandler.text;
            o = JSON.Parse(json);

            foreach (var item in o) {
                var itemo = JSON.Parse(item.ToString());
                // updateText += itemo[0]["Artwork_Name"] + ": " + itemo[0]["Upload_Artwork"] + "\n";
                maxID = itemo[0]["ID"];
            }
            // var lastRow = o[o.size() - 1];
            // var ii = JSON.Parse(lastRow.ToString());
            // maxID = ii[0]["ID"];
            //print(maxID);

            // outputArea.text = updateText;
        }
    }
}
