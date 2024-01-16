using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GunAttackStartegy
{
    public Player player;
    public Weapon Hit
    {
        get
        {
            return player.weapon;
        }
    }
    public GunAttackStartegy(Player player) 
    {

        this.player = player;
    }

    public abstract void ShootGun();
}

public class PistolAttackStartegy : GunAttackStartegy
{
    public PistolAttackStartegy(Player player) : base(player)
    {

    }

    public void Shoot()
    {
        Debug.Log("Pistol Shot");
        Hit.hitPoint.Hit( Hit.damage, Hit.maxLength);
        Hit.BulletCount--;
    }

    public override void ShootGun()
    {
        Shoot();
    }
}
public class ShotgunAttackStartegy : GunAttackStartegy
{
    public ShotgunAttackStartegy(Player player) : base(player)
    {

    }
    public void Shoot()
    {
        Debug.Log("Shotgun Shot");
        Hit.hitPoint.Hit(Hit.damage, Hit.maxLength);
        Hit.BulletCount--;
    }
    public override void ShootGun()
    {
        Shoot();
    }
}

