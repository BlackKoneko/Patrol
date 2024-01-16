using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentItem : Item
{
    public override void Use(Player player)
    {
        player.ChangeWeapon(this.gameObject);
    }
}
