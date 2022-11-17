using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatesLevel : MonoBehaviour
{
    public PlayerController playerController;
    public TextMeshProUGUI healthLevelTxt;
    public TextMeshProUGUI moveLevelTxt;
    int healthLevel, moveLevel;
    public int healthCoin, moveCoin;
    public GameObject menuCanvas;
    public Button healthBtn, moveBtn;
    public Image surfaceImg;
    private Color initColor;
    public Color activeColor;
    public GameObject fightBtn;
    public ParticleSystem levelUpparticle;
    void Start()
    {
        healthLevel = 1;
        moveLevel = 1;
        initColor = surfaceImg.color;
        healthLevelTxt.text = "Health " + healthLevel + '\n' + (healthCoin * healthLevel).ToString();
        moveLevelTxt.text = "Speed " + moveLevel + '\n' + (moveCoin * moveLevel).ToString();
        menuCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void MoveUp()
    {
        playerController.speed += 0.2f;
        GameManager.Instance.RemoveCoin((moveCoin * moveLevel));
        moveLevel++;
        moveLevelTxt.text = "Speed " + moveLevel + '\n' + (moveCoin * moveLevel).ToString();
        levelUpparticle.Play();
        ButtonsCheck();
    }
    public void HealthUp()
    {
        playerController.initHealth += 1;
        playerController.AddHealth();
        GameManager.Instance.RemoveCoin((healthCoin *healthLevel));
        healthLevel++;
        healthLevelTxt.text = "Health " + healthLevel + '\n' + (healthCoin *healthLevel).ToString();
        levelUpparticle.Play();
        ButtonsCheck();
    }
    public void ButtonsCheck()
    {
        if (GameManager.Instance.coins >= (healthCoin * healthLevel))
        {
            healthBtn.interactable = true;
        }
        else
        {
            healthBtn.interactable = false;
        }
        if (GameManager.Instance.coins >= (moveCoin * moveLevel))
        {
            moveBtn.interactable = true;
        }
        else
        {
            moveBtn.interactable = false;
        }
        MaxLevelCheck();
    }
    void MaxLevelCheck()
    {
        if (healthLevel == 8)
        {
            healthBtn.interactable = false;
        }
        if(moveLevel == 15)
        {
            moveBtn.interactable= false;
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
