using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GateUpControll: MonoBehaviour
{
    public PlayerController playerController;
    public DoorControll[] doorControll;
    public TextMeshProUGUI gateLevelTxt;
    int gateLevel;
    public int gateCoin;
    public GameObject menuCanvas;
    public Button gateBtn;
    public Image surfaceImg;
    private Color initColor;
    public Color activeColor;
    public GameObject fightBtn;
    public ParticleSystem levelUpparticle;
    void Start()
    {
        doorControll = FindObjectsOfType<DoorControll>();
        gateLevel = 1;
        initColor = surfaceImg.color;
        gateLevelTxt.text = "Gates " + gateLevel + '\n' + (gateCoin * gateLevel).ToString();
        menuCanvas.SetActive(false);
    }
    public void GateUp()
    {
        for (int i = 0; i < doorControll.Length; i++)
        {
            doorControll[i].LevelUpDoor();
        }
        gateLevel++;
        GameManager.Instance.RemoveCoin((gateCoin * gateLevel));
        gateLevelTxt.text = "Gates " + gateLevel + '\n' + (gateCoin * gateLevel).ToString();
        levelUpparticle.Play();
        ButtonsCheck();
    }
    public void ButtonsCheck()
    {
        if (GameManager.Instance.coins >= (gateCoin * gateLevel))
        {
            gateBtn.interactable = true;
        }
        else
        {
            gateBtn.interactable = false;
        }
        MaxLevelCheck();
    }
    void MaxLevelCheck()
    {
        if (gateLevel == 4)
        {
            gateLevelTxt.text = "Gates " + gateLevel + '\n' + "Max";
            gateBtn.interactable = false;
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
