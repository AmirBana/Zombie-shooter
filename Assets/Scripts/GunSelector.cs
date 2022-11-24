using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSelector : MonoBehaviour
{
    public GameObject[] guns;
    void Start()
    {
        for(int i = 0; i < guns.Length; i++)
        {
            guns[i].SetActive(false);
        }
        guns[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SelectGun(int n)
    {
        for(var i = 0; i < guns.Length; i++)
        {
            if (guns[i].activeInHierarchy == true) guns[i].SetActive(false);
        }
        guns[n].SetActive(true);
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
    public void ClosePanel()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
}
