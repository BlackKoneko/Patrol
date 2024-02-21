using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class Weapon : MonoBehaviour
{ 
    public enum Weapon_Type
    {
        PISTOL,
        SHOTGUN
    }
    public int damage;
    public int bulletCount;
    public int bulletCountMax;
    public float maxLength;
    public HitPoint hitPoint;
    public bool fire = true;
    public Weapon_Type weapon_Type;
    GunAttackStartegy gunAttackStartegy;
    public AudioClip shootAudio;

    private void Start()
    {
        gunAttackStartegy = new PistolAttackStartegy(this);
        hitPoint = GetComponentInChildren<HitPoint>();
        BulletCount = bulletCountMax;
    }

    private void Update()
    {
        switch(weapon_Type)
        {
            case Weapon_Type.PISTOL:
                gunAttackStartegy = new PistolAttackStartegy(this);
                break;

            case Weapon_Type.SHOTGUN:
                gunAttackStartegy = new ShotgunAttackStartegy(this);
                break;
        }
    }
    public void Shoot()
    {
        gunAttackStartegy.ShootGun();
        SoundManager.instance.Play(shootAudio, transform, false);
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
