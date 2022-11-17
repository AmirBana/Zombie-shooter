using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    public List<GameObject> hearts = new List<GameObject>();
    Transform player;
    GridLayoutGroup grid;
    float initCellSize;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        grid = GetComponentInChildren<GridLayoutGroup>();
        initCellSize = grid.cellSize.x;
        HealthSet();
    }
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.main.transform.position);
        transform.position = new Vector3(player.position.x, transform.position.y, player.position.z);
    }
    public void LostHeart(int i)
    {
        Color32 color = hearts[i].GetComponent<Image>().color;
        color.a = 0;
        hearts[i].GetComponent<Image>().color = color;
    }
    public void HealthUpgrade(int health)
    {
        GameObject newHeart = Instantiate(hearts[0], transform.position, transform.rotation, GetComponentInChildren<GridLayoutGroup>().transform);
        for (int i = 0; i < hearts.Count; i++)
        {
            Color32 color = hearts[i].GetComponent<Image>().color;
            color.a = 255;
            hearts[i].GetComponent<Image>().color = color;
        }
        hearts.Add(newHeart);
        HealthSet();
    }
    void HealthSet()
    {
        float xPos = initCellSize/(hearts.Count);
        grid.cellSize = new Vector2 (xPos, grid.cellSize.y);
    }
}
