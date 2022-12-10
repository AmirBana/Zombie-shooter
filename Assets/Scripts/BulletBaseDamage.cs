using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct GunBullets {
    public Bullet item;
    public int baseDamage;
}

public class BulletBaseDamage : MonoBehaviour
{
    public GunBullets[] bullets;
    void Start()
    {
        foreach(var bullet in bullets)
        {
            bullet.item.damage = bullet.baseDamage;
        }
    }
}
