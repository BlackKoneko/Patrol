using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GunAttackStartegy
{
    public Weapon weapon;
    public GunAttackStartegy(Weapon weapon) 
    {

        this.weapon = weapon;
    }

    public abstract void ShootGun();
}

public class PistolAttackStartegy : GunAttackStartegy
{
    public PistolAttackStartegy(Weapon weapon) : base(weapon){}
    public override void ShootGun()
    {
        weapon.hitPoint.Hit(weapon.damage, weapon.maxLength);
        weapon.BulletCount--;
    }
}
public class ShotgunAttackStartegy : GunAttackStartegy
{
    public ShotgunAttackStartegy(Weapon weapon) : base(weapon){}

    public override void ShootGun()
    {
        weapon.hitPoint.Hit(weapon.damage, weapon.maxLength);
        weapon.BulletCount--;
    }
}

