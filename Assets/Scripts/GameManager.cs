using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public EnemySpawner[] spawners;
    public Button fightBtn;
    public TextMeshProUGUI coinTxt,waveTxt;
    public int coins;
    [HideInInspector] public int waveNumber;
    public bool gameOver;
    public GameObject inGamePanel, gameOverPanel, weaponSelectPanel;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        waveNumber = 1;
        WaveEnd();
        spawners = FindObjectsOfType<EnemySpawner>();
        inGamePanel.SetActive(true);
        gameOverPanel.SetActive(false);
        gameOver = false;
    }
    public void WaveShow()
    {
        waveTxt.text = "Wave " + waveNumber;
        waveTxt.gameObject.SetActive(true);
        Invoke("WaveEnd", 2f);
    }
    public void WaveEnd()
    {
        waveTxt.gameObject.SetActive(false);
    }
    public void StartWave()
    {
        WaveShow();
        for (int i = 0; i < spawners.Length; i++)
        {
            StartCoroutine(spawners[i].Spawner());
        }
        fightBtn.gameObject.SetActive(false);
    }
    public void WaveFinished()
    {
        waveNumber++;
        fightBtn.gameObject.SetActive(true);
        for (int i = 0; i < spawners.Length; i++)
        {
            if (spawners[i].spawned > 0)
            {
                spawners[i].totalSpawn += 5;
            }
            spawners[i].spawned = 0;
        }
    }
    public void AddCoin(int amount)
    {
        coins += amount;
        coinTxt.text = coins.ToString();
    }
    public void RemoveCoin(int amount)
    {
        coins -= amount;
        coinTxt.text = coins.ToString();
    }
    public void GameOver()
    {
        gameOver = true;
        inGamePanel.SetActive(false);
        gameOverPanel.SetActive(true);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void OpenWeaponPanel()
    {
        Time.timeScale = 0;
        weaponSelectPanel.SetActive(true);
    }
}