using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public int damage;
    void Start()
    {
        StartCoroutine("LifeTime"); 
    }
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("zombieEnter") || other.CompareTag("DeadEnemy") || other.CompareTag("Bullet") || other.CompareTag("Player") || other.CompareTag("Ally")){ }
        else Destroy(gameObject);
    }
}
