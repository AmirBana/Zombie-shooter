using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class NewRoomOpened : MonoBehaviour
{
    public int price;
    public TextMeshProUGUI priceTxt;
    public Image img;
    public Image img2;
    public Color32 collideColor;
    public Color32 mainColor;
    bool isInZone;
    public float waitTime;
    public GameObject nextRoom;
    public float createTime;
    public GameObject gateEnterance;
    public GameObject[] Replacables;
    void Start()
    {
        mainColor = img.color;
        priceTxt.text = price.ToString();
    }
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            img.color = collideColor;
            isInZone = true;
            if(GameManager.Instance.coins > price)
            {
                StartCoroutine("FillGreen");
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            img.color = mainColor;
            isInZone = false;
            StopCoroutine("FillGreen");
            img2.rectTransform.localScale = Vector3.zero;
        }
    }
    IEnumerator FillGreen()
    {
        while(isInZone && img2.rectTransform.localScale.x < 1)
        {
            yield return new WaitForSeconds(0.1f);
            Vector3 imgScale = img2.rectTransform.localScale;
            img2.rectTransform.localScale = new Vector3(imgScale.x + (0.1f / waitTime), imgScale.y + (0.1f / waitTime), imgScale.z);
        }
        if(img2.rectTransform.localScale.x >= 1)
        {
            nextRoom.SetActive(true);
            GameManager.Instance.RemoveCoin(price);
            StartCoroutine(OpenRoom());
        }
    }
    IEnumerator OpenRoom()
    {
        while(nextRoom.transform.localScale.x < 1)
        {
            yield return new WaitForSeconds(0.1f);
            Vector3 roomScale = nextRoom.transform.localScale;
            nextRoom.transform.localScale = new Vector3(roomScale.x + (0.1f/createTime),roomScale.y + (0.1f/createTime), roomScale.z + (0.1f/createTime));
        }
        if(gateEnterance != null)  gateEnterance.SetActive(false);
        this.gameObject.SetActive(false);
        for(int i = 0; i < Replacables.Length; i++)
        {
            if (Replacables[i].activeInHierarchy) Replacables[i].SetActive(false);
            else Replacables[i].SetActive(true);
        }
    }
}
