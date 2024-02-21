using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Proximity : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private float dist;
    [SerializeField] private TextMeshPro text;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        text = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(player.transform.position, transform.position);
        text.fontSize = Mathf.Clamp(1 / dist * 40f, 5, 15);
        //text.transform.rotation = Quaternion.Slerp(text.transform.rotation, player.transform.rotation, 0.05f);
        //text.transform.LookAt(new Vector3(transform.position.x, transform.position.y, transform.position.z));
       // if (dist < 4f)
        //{
        //    StopAllCoroutines();
        //    StartCoroutine(Grow(text));
       // }
        //else
        //{
        //    StopAllCoroutines();
        //    StartCoroutine(Shrink(text));
       // }

    }

    IEnumerator FadeIn(TextMesh t)
    {
        for(int i = 0; i < 90; i++)
        {
            t.color = new Color(t.color.r, t.color.g, t.color.b, t.color.a + 1);
            yield return new WaitForSeconds(0.4f);
        }
    }

    IEnumerator Shrink(TextMesh t)
    {
        for (int i = 0; i < 90; i++)
        {
            t.fontSize -= 1;
            yield return new WaitForSeconds(0.4f);
        }
        
    }
}
