using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform doorTarget;
    public Transform playerTarget;
    public int life;
    public EnemyList enemyList;
    public int killPrice;
    Animator anim;
    public Material enemyDeadMat;
    public GameObject model;
    public ParticleSystem blood;
    void Start()
    {
        enemyList.allEnemies.Add(this);
        playerTarget = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        agent.SetDestination(doorTarget.position);  
    }

    // Update is called once per frame
    void Update()
    {
        if (doorTarget.GetComponent<DoorControll>().isDestroyed && !GameManager.Instance.gameOver)
        {
            agent.SetDestination(playerTarget.position);
        }
        if(GameManager.Instance.gameOver == true)
        {
            agent.isStopped = true;
        }
        if(agent.velocity.magnitude > 0.1f)
        {
            anim.SetBool("Run", true);
        }
        else
        {
            anim.SetBool("Run", false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {   if (gameObject.CompareTag("DeadEnemy")) return;
        if(other.CompareTag("Bullet"))
        {
            life -= other.gameObject.GetComponent<Bullet>().damage; 
            Destroy(other.gameObject);
            blood.Play();
            print(life);
            if (life <= 0)
            {
                if(gameObject.CompareTag("Enemy"))
                {
                    PlayerDeath();
                }
            }
        }
    }
    void PlayerDeath()
    {
        anim.SetBool("Death", true);
        gameObject.tag = "DeadEnemy";
        gameObject.layer = 0;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        agent.isStopped = true;
        model.GetComponent<SkinnedMeshRenderer>().material = enemyDeadMat;
        enemyList.allEnemies.Remove(this);
        if (enemyList.allEnemies.Count == 0)
        {
            GameManager.Instance.WaveFinished();
        }
        GameManager.Instance.AddCoin(killPrice);
        Destroy(gameObject, 2f);
    }
}
