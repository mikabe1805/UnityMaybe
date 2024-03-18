using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // UnlockMouse();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UnlockMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameObject.Find("PlayerCapsule").GetComponent<StarterAssets.StarterAssetsInputs>().cursorInputForLook = true;
    }
}
