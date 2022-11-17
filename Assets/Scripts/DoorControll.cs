using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControll : MonoBehaviour
{
    public List<GameObject> woods = new List<GameObject>();
    public List<GameObject> breakedWoods = new List<GameObject>();
    public List<GameObject> levelUpWoods = new List<GameObject> ();
    public bool isDestroyed;
    public bool isFixed;
    private bool isTakingDmg;
    private Collider collider1;
    public LayerMask enemyLayer;
    void Start()
    {
        isTakingDmg = false;
        isFixed = true;
        isDestroyed = false;
        collider1 = GetComponent<Collider>();
    }
    // Update is called once per frame
    void Update()
    {
        if(woods.Count <= 0) isDestroyed = true;
    }
    private void FixedUpdate()
    {
        var enemyHit = Physics.OverlapBox(collider1.bounds.center, transform.localScale,transform.rotation,enemyLayer);
        if (enemyHit.Length > 0)
        {
            if (!isTakingDmg)
            {
                StartCoroutine("TakeDamage");
                isTakingDmg = true;
            }
        }
        else
        {
            StopCoroutine("TakeDamage");
            isTakingDmg = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && GameManager.Instance.fightBtn.gameObject.activeInHierarchy == true)
        {
            StartCoroutine("fixDoor");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && GameManager.Instance.fightBtn.gameObject.activeInHierarchy == true)
        {
            StopCoroutine("fixDoor");
        }
    }
    IEnumerator TakeDamage()
    {
        while(!isDestroyed)
        {
            yield return new WaitForSeconds(1f);
            isFixed = false;
            woods[woods.Count - 1].gameObject.SetActive(false);
            breakedWoods.Add(woods[woods.Count - 1]);
            woods.RemoveAt(woods.Count - 1);
            if (woods.Count <= 0) isDestroyed = true;
        }
    }
    IEnumerator fixDoor()
    {
        while(!isFixed)
        {
            yield return new WaitForSeconds(0.1f);
            breakedWoods[breakedWoods.Count - 1].gameObject.SetActive(true);
            woods.Add(breakedWoods[breakedWoods.Count - 1]);
            breakedWoods.RemoveAt(breakedWoods.Count - 1);
            if (woods.Count > 0) isDestroyed = false;
            if(breakedWoods.Count <= 0) isFixed = true;
        }
    }
    public void LevelUpDoor()
    {
        for(int i = 0;i<breakedWoods.Count;i++)
        {
            breakedWoods[breakedWoods.Count - 1].gameObject.SetActive(true);
            woods.Add(breakedWoods[breakedWoods.Count - 1]);
            breakedWoods.RemoveAt(breakedWoods.Count - 1);
            if (woods.Count > 0) isDestroyed = false;
            if (breakedWoods.Count <= 0) isFixed = true;
        }
        levelUpWoods[0].SetActive(true);
        woods.Add(levelUpWoods[0]);
        levelUpWoods.RemoveAt(0);
    }
}
