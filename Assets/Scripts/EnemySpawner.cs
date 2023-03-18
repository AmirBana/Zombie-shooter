using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public bool isActive;
    public GameObject[] enemies;
    public Transform doorTarget;
    public GameObject room;
    public float enemyYPos;
    private float zMin, zMax, xMin, xMax;
    public float zPosOffset, xPosOffset;
    Vector3 spawnPos;
    public float spawnTime;
    public float numberInRound;
    public float totalSpawn;
    [HideInInspector] public int spawned;
    public EnemyList enemyList;
    void Start()
    {
        spawnPos = new Vector3(transform.position.x, enemyYPos, 0f);
        zMin = transform.position.z - 5;
        zMax = transform.position.z + 5;
        xMin = transform.position.x - 5;
        xMax = transform.position.x + 5;
        spawned = 0;
    }
    void Update()
    {

    }
    public IEnumerator Spawner()
    {
        if (room.transform.localScale.x >= 1)
        {
            while (spawned < totalSpawn)
            {
                for (int i = 0; i < numberInRound; i++)
                {
                    spawnPos.z = Random.Range(zMin, zMax);
                    spawnPos.x = Random.Range(xMin, xMax);
                    GameObject obj;
                    if (spawned <= 15)
                    {
                        obj = Instantiate(enemies[0], spawnPos, enemies[0].transform.rotation);
                    }
                    else if (spawned <= 25)
                    {
                        obj = Instantiate(enemies[1], spawnPos, enemies[1].transform.rotation);
                    }
                    else if (spawned <= 35)
                    {
                        obj = Instantiate(enemies[2], spawnPos, enemies[2].transform.rotation);
                    }
                    else
                    {
                        obj = Instantiate(enemies[3], spawnPos, enemies[3].transform.rotation);
                    }
                    obj.GetComponent<Enemy>().doorTarget = doorTarget;
                    obj.GetComponent<Enemy>().enemyList = enemyList;
                    spawned++;
                }
                yield return new WaitForSeconds(spawnTime);
            }
        }
        else yield return null;
    }
}
