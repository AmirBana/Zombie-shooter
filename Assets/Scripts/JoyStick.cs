using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class JoyStick : MonoBehaviour
{
    public RectTransform joyBG;
    public RectTransform joyMain;
    [Range(0.1f,5f)] public float range;
    void Start()
    { 
    }

    // Update is called once per frame
    void Update()
    {
        JoyStartPos();
    }
    void JoyStartPos()
    {
        Touch touch;
        if(Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            Vector3 pos;
            if(touch.phase == TouchPhase.Began)
            {
                pos = touch.position;
                joyBG.position = pos;
            }
            if(touch.phase == TouchPhase.Moved)
            {
                pos = touch.position;
                joyMain.position = pos;
                if(joyMain.position.x < 0)
                {
                    joyMain.position = new Vector3(0, joyMain.position.y, joyMain.position.z);
                }
                if(joyMain.position.y < 0)
                {
                    joyMain.position = new Vector3(joyMain.position.x, 0, joyMain.position.z);
                }
            }
        }
    }
}
