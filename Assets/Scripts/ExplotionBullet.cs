using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplotionBullet : MonoBehaviour
{
    public GameObject explotion;

    private void OnDestroy()
    {
        print("cleared");
        Instantiate(explotion, transform.position, explotion.transform.rotation);
    }
}
