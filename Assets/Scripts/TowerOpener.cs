using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
public class TowerOpener : MonoBehaviour
{
    [Header("Canvas")]
    public Image img;
    public GameObject btnCanvasStart;
    public GameObject btnCanvasLvlUp;
    public Color32 collideColor;
    public Color32 mainColor;
    public GameObject tower;
    public float createTime;
    public int price1;
    public int price2;
    [Header("Soldier")]
    public GameObject[] soldiers;
    public Vector3 objPos;
    public int currLevel;
    GameObject currObject;
    void Start()
    {
        mainColor = img.color;
        currLevel = 0;
    }
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            img.color = collideColor;
            if (currLevel == 0)
                btnCanvasStart.SetActive(true);
            else
                btnCanvasLvlUp.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            img.color = mainColor;
            if (currLevel == 0)
            {
                btnCanvasStart.SetActive(false);
            }
                
            else
            {
                btnCanvasLvlUp.SetActive(false);
            }
               
        }
    }
    public void CreateTower()
    {
        if (GameManager.Instance.coins >= price1)
        {
            StartCoroutine(OpenRoom());
            GameManager.Instance.RemoveCoin(price1);
        }
    }
    public void LevelUp()
    {
        if (GameManager.Instance.coins < price2) return;
        if (currObject != null) Destroy(currObject);
        GameManager.Instance.RemoveCoin(price2);
        GameObject obj = Instantiate(soldiers[currLevel], objPos, soldiers[currLevel].transform.rotation, tower.transform);
        obj.transform.localPosition = objPos;
        currObject = obj;
        currLevel += 1;
        if (currLevel == soldiers.Length)
        {
            this.gameObject.SetActive(false);
        }
    }
    IEnumerator OpenRoom()
    {
        btnCanvasStart.SetActive(false);
        while (tower.transform.localScale.x < 1)
        {
            yield return new WaitForSeconds(0.01f);
            Vector3 roomScale = tower.transform.localScale;
            tower.transform.localScale = new Vector3(roomScale.x + (0.01f / createTime), roomScale.y + (0.01f / createTime), roomScale.z + (0.01f / createTime));
        }
      //  btnCanvasLvlUp.SetActive(true);
        LevelUp();
    }
}
