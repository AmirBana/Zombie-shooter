using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSoldier : MonoBehaviour
{
    public GameObject bullet;
    public Transform firePos;
    public float range;
    public float fireRate;
    public LayerMask enemyLayer;
    bool doFire;
    void Start()
    {
        StartCoroutine("FireBullet");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        EnemyDetect();
    }
    void EnemyDetect()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, range, enemyLayer);
        float distance = 1000f;
        Collider enemy=null;

        if (enemies.Length <= 0) doFire = false;
        if (enemies.Length > 0)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                float tempDistance = Vector3.Distance(transform.position, enemies[i].transform.position);
                if (tempDistance < distance)
                {
                    distance = tempDistance;
                    enemy = enemies[i];
                }
            }
            transform.LookAt(new Vector3(enemy.transform.position.x, transform.position.y, enemy.transform.position.z));
            firePos.LookAt(enemy.transform.position);
            doFire = true;
        }

    }
    IEnumerator FireBullet()
    {
        while (true)
        {
            yield return new WaitForSeconds(fireRate);
            if (doFire)
            {
                Instantiate(bullet, firePos.position, firePos.rotation);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
