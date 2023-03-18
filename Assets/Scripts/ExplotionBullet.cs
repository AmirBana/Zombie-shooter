using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplotionBullet : MonoBehaviour
{
    public GameObject explotion;
    public int bombRange;
    private void Start()
    {

    }
    private void OnDestroy()
    {
        //Damage();
 
    }
    private void Damage()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position,bombRange);
        Instantiate(explotion, transform.position, explotion.transform.rotation);
        for (int i=0;i<enemies.Length;i++)
        {
            enemies[i].GetComponent<Enemy>().life -= GetComponent<Bullet>().damage;
        }
        Destroy(gameObject);
    }
}
