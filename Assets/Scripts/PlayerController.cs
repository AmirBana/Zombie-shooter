using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    public HealthBar healthBar;
    public FloatingJoystick joystick;
    public GameObject joystickBg;
    public int initHealth;
    int health;
    private Animator anim;
    public float speed;
    public float rotSpeed;
    public GameObject bullet;
    public Transform firePos;
    public ParticleSystem gunFire;
    public float range;
    public float range2;
    public float fireRate;
    public float gunSpeed;
    public float dmgTimeOut;
    public LayerMask enemyLayer;
    bool doFire;
    bool canTakeDmg;
    float timeCount;
    // Start is called before the first frame update
    void Start()
    {
        health = initHealth;
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        //gunFire = firePos.GetComponentsInChildren<ParticleSystem>()[0];
        StartCoroutine("FireBullet");
        canTakeDmg = true;
        fireRate = 0.3f;
    }
    private void Update()
    {

    }
    private void FixedUpdate()
    {
        Movement();
        EnemyDetect();
    }
    void Movement()
    {
        //float horizontal = Input.GetAxis("Horizontal")*speed*Time.deltaTime;
        //float vertical = Input.GetAxis("Vertical")*speed*Time.deltaTime;
        float horizontal = joystick.Horizontal*speed*Time.deltaTime;
        float vertical = joystick.Vertical*speed*Time.deltaTime;
        Vector3 movePos = new Vector3(horizontal, 0, vertical);
        controller.Move(movePos);   
        RotatePlayer(vertical,horizontal);
        if(vertical != 0 || horizontal != 0)
        {
            anim.SetBool("Walk", true);
            anim.SetFloat("Speed", Mathf.Max(Mathf.Abs(joystick.Horizontal),Mathf.Abs(joystick.Vertical)));
        }
        else
        {
            anim.SetBool("Walk", false);
          //anim.SetFloat("Speed", 0);
        }
    }
    void RotatePlayer(float v,float h)
    {
        if (!doFire)
        {
            if(joystickBg.activeSelf == true)
            {
                Vector2 dir = joystick.Direction;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, -angle + 90, 0), timeCount + Time.deltaTime * rotSpeed);
            }

        }
    }
    IEnumerator FireBullet()
    {
        while(true)
        {
            yield return new WaitForSeconds(fireRate*gunSpeed);
            if (doFire)
            {
                Instantiate(bullet, firePos.position, firePos.rotation);
                gunFire.Play();
            }
        }
    }
    void EnemyDetect()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, range, enemyLayer);
       // Collider[] enemies2 = Physics.OverlapSphere(transform.position, range2, enemyLayer);
        if (enemies.Length > 0)
        {
            Collider enemy = enemies[0];
            Vector3 forward = enemy.transform.position - transform.position;
            forward.y = 0;
            doFire = true;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(forward), timeCount + Time.deltaTime * rotSpeed);
        }
        else if (enemies.Length <= 0) doFire = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (canTakeDmg)
            if (other.gameObject.CompareTag("Enemy"))
            {
                health--;
                healthBar.LostHeart(health);
                canTakeDmg = false;
                StartCoroutine("TakeDmg");
                if (health == 0)
                    PlayerDie();
            }
    }
    void PlayerDie()
    {
        Destroy(gameObject);
        healthBar.gameObject.SetActive(false);
        GameManager.Instance.GameOver();
    }
    public void AddHealth()
    {
        health = initHealth;
        healthBar.HealthUpgrade(initHealth);
    }
    IEnumerator TakeDmg()
    {
        yield return new WaitForSeconds(dmgTimeOut);
        canTakeDmg = true;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
