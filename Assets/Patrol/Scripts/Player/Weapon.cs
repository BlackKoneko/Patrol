using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public enum Weapon_Type
{
    PISTOL,
    SHOTGUN,
    ROKET
}

public class Weapon : MonoBehaviour
{ 
    public int damage;
    public int bulletCount;
    public int bulletCountMax;
    public float maxLength;
    public HitPoint hitPoint;
    public bool fire = true;
    public Weapon_Type weapon_Type;
    
    private void Start()
    {
        hitPoint = GetComponentInChildren<HitPoint>();
        BulletCount = bulletCountMax;
    }
    public int BulletCount
    {
        get { return bulletCount; }
        set
        {
            bulletCount = value;
            if (bulletCount < 0)
            {
                bulletCount = 0;
            }
            if (bulletCount > bulletCountMax)
            {
                bulletCount = bulletCountMax;
            }
        }
    }
    
}
