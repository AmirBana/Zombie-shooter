using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AllyAdder : MonoBehaviour
{
    public PlayerController playerController;
    public GameObject ally;
    public TextMeshProUGUI allyLevelTxt;
    int allyNum;
    public int allyCoin;
    public GameObject menuCanvas;
    public Button allyBtn;
    public Image surfaceImg;
    private Color initColor;
    public Color activeColor;
    public GameObject fightBtn;
    void Start()
    {
        initColor = surfaceImg.color;
        allyLevelTxt.text = "Ally"+ '\n' + (allyCoin +(50*allyNum)).ToString();
        menuCanvas.SetActive(false);
    }
    public void AddAlly()
    {
        allyNum++;
        GameManager.Instance.RemoveCoin((allyCoin + (50* allyNum)));
        allyLevelTxt.text = "Ally" + '\n' + (allyCoin + (50* allyNum)).ToString();
        Adder();
        ButtonsCheck();
    }
    private void Adder()
    {
        Instantiate(ally, transform.position, ally.transform.rotation);
    }
    public void ButtonsCheck()
    {
        if(allyNum>=3)
        {
            allyLevelTxt.text = "Ally\nMax";
            allyBtn.interactable = false;
        }
        if (GameManager.Instance.coins >= (allyCoin + (50* allyNum)))
        {
            allyBtn.interactable = true;
        }
        else
        {
            allyBtn.interactable = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (fightBtn.activeInHierarchy == true)
            {
                ButtonsCheck();
                surfaceImg.color = activeColor;
                menuCanvas.SetActive(true);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            surfaceImg.color = initColor;
            menuCanvas.SetActive(false);
        }
    }
}
