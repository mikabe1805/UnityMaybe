using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Resize : MonoBehaviour
{
    public Image image;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void resize()
    {
        Sprite sprite = image.sprite;
        Texture texture = sprite.texture;
         float x;
         float y;
         if (texture.height < texture.width) {
            float ratio = (float)texture.height / texture.width;
            // print("ratio: " + ratio);
            x = 1; //width
            y = x * ratio;
         } else {
            float ratio = (float)texture.width / texture.height;
            // print("ratio: " + ratio);
            y = 1; //height
            x = y * ratio;
         }
         gameObject.transform.localScale = new Vector2(x, y);
    }
}
