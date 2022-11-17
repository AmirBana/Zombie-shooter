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
    public TextMeshProUGUI coinTxt;
    public int coins;
    public bool gameOver;
    public GameObject inGamePanel, gameOverPanel;
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
        spawners = FindObjectsOfType<EnemySpawner>();
        inGamePanel.SetActive(true);
        gameOverPanel.SetActive(false);
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartWave()
    {
         for(int i = 0; i < spawners.Length; i++)
         {
            StartCoroutine(spawners[i].Spawner());
         }
         fightBtn.gameObject.SetActive(false);
    }
    public void WaveFinished()
    {
        fightBtn.gameObject.SetActive(true);
        for(int i = 0; i < spawners.Length; i++)
        {
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
}