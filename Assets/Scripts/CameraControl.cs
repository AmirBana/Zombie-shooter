using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform player;
    Vector3 offset;
    void Start()
    {
        offset = transform.position - player.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float xPos = player.position.x + offset.x;
        float zPos = player.position.z + offset.z;
        transform.position = new Vector3(xPos,transform.position.y, zPos);

    }
}
