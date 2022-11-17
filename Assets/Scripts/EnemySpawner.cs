using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public bool isActive;
    public GameObject enemy;
    public Transform doorTarget;
    public GameObject room;
    public float enemyYPos;
    private float zMin, zMax,xMin,xMax;
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
                    GameObject obj = Instantiate(enemy, spawnPos, enemy.transform.rotation);
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
