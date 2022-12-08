using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PlayerAlly : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject player;
    public GameObject bullet;
    public Transform firePos;
    public float range;
    public float fireRate;
    public LayerMask enemyLayer;
    private bool doFire;
    public float rotSpeed;
    private float timeCount;
    private Animator anim;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        StartCoroutine("FireBullet");
    }

    void Update()
    {
        agent.SetDestination(player.transform.position);
        if (agent.velocity.magnitude > 0.1f)
        {
            anim.SetBool("Walk", true);
            anim.SetFloat("Speed", agent.velocity.magnitude);
        }
        else
        {
            anim.SetBool("Walk", false);
        }
    }
    private void LateUpdate()
    {
        EnemyDetect();
    }
    private bool WallDetect()
    {
        RaycastHit hit;
        if(Physics.Raycast(firePos.transform.position,firePos.forward,out hit,Mathf.Infinity))
        {
            if(hit.transform.name.Contains("Wall"))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        return true;
    }
    private void EnemyDetect()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, range, enemyLayer);
        float distance = 1000f;
        Collider enemy = null;
        if (enemies.Length <= 0) doFire = false;
        if(enemies.Length > 0)
        {
            for(int i=0; i < enemies.Length; i++)
            {
                float tempDistance = Vector3.Distance(transform.position, enemies[i].transform.position);
                if(tempDistance<distance)
                {
                    distance = tempDistance;
                    enemy = enemies[i];
                }
            }
            Vector3 forward = enemy.transform.position - transform.position;
            forward.y = 0;
            doFire = WallDetect();
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(forward), timeCount + Time.deltaTime * rotSpeed);
        }
    }
    IEnumerator FireBullet()
    {
        while(true)
        {
            yield return new WaitForSeconds(fireRate);
            if(doFire)
            {
                Instantiate(bullet, firePos.position, firePos.rotation);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
