using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class manageVideo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(playVid());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator playVid() 
    {
        yield return new WaitForSeconds(8);
        this.gameObject.SetActive(false);
    }
}
