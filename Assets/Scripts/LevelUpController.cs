using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUpController : MonoBehaviour
{
    public PlayerController playerController;
    public Bullet bullet;
    public TextMeshProUGUI fireLevelTxt;
    public TextMeshProUGUI damageLevelTxt;
    int fireLevel,damageLevel;
    public int fireCoin, damageCoin;
    public GameObject menuCanvas;
    public Button dmgBtn, firerateBtn;
    public Image surfaceImg;
    private Color initColor;
    public Color activeColor;
    public GameObject fightBtn;
    public ParticleSystem levelUpparticle;
    void Start()
    {
        fireLevel = 1;
        damageLevel = 1;
        initColor = surfaceImg.color;
        fireLevelTxt.text = "Fire Rate " + fireLevel + '\n' + (fireCoin * fireLevel).ToString();
        damageLevelTxt.text = "Damage " + damageLevel + '\n' + (damageCoin * damageLevel).ToString();
        menuCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
   
    }
    public void DamgeUp()
    {
        bullet.damage += 1;
        GameManager.Instance.RemoveCoin((damageCoin * damageLevel));
        damageLevel++;
        damageLevelTxt.text = "Damage "+damageLevel+'\n'+(damageCoin*damageLevel).ToString();
        levelUpparticle.Play();
        ButtonsCheck();
    }
    public void FireRateUp()
    {
        if(playerController.fireRate > 0.02f)
        {
            playerController.fireRate -= 0.01f;
        }
        GameManager.Instance.RemoveCoin((fireCoin * fireLevel));
        fireLevel++;
        fireLevelTxt.text = "Fire Rate "+fireLevel+'\n' + (fireCoin * fireLevel).ToString();
        levelUpparticle.Play();
        ButtonsCheck();
    }
    public void ButtonsCheck()
    {
        if(GameManager.Instance.coins >= (damageCoin*damageLevel))
        {
            dmgBtn.interactable = true;
        }
        else
        {
            dmgBtn.interactable = false;
        }
        if(GameManager.Instance.coins >= (fireCoin*fireLevel))
        {
            firerateBtn.interactable = true;
        }
        else
        {
            firerateBtn.interactable = false;
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
        if(other.CompareTag("Player"))
        {
                surfaceImg.color = initColor;
                menuCanvas.SetActive(false);
        }
    }
}
