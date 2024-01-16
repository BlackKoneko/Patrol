using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CONSUMABLE_TYPE
{
    POTION,
    PISTOL_BULLET,
    SHOTGUN_BULLET,
    KEY
}

public class ConsumableItem : Item
{
    public CONSUMABLE_TYPE type;
    public bool isUseable = true;

    public override void Use(Player player)
    {
        switch(type)
        {
            case CONSUMABLE_TYPE.POTION:
                player.Hp += value;
                isUseable = false;
                Destroy(gameObject);
                break;
            case CONSUMABLE_TYPE.PISTOL_BULLET:
                AddBullet(0, player);
                break;
            case CONSUMABLE_TYPE.SHOTGUN_BULLET:
                if(player.weapons[1] != null)
                {
                    AddBullet(1, player);
                }
                break;
            case CONSUMABLE_TYPE.KEY:
                player.keynumber++;
                isUseable = false;
                Destroy(gameObject);
                break;
        } 
    }

   public void AddBullet(int weaponArrayNum, Player player)
    {
        int gunReduceValue = (player.weapons[weaponArrayNum].GetComponent<Weapon>().bulletCountMax - player.weapons[weaponArrayNum].GetComponent<Weapon>().BulletCount);

        player.weapons[weaponArrayNum].GetComponent<Weapon>().BulletCount += value;
        value -= gunReduceValue;
        Debug.Log(value);
        if (value <= 0)
        {
            Destroy(gameObject);
            isUseable = false;
        }
    }
}
