using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour, IHitable
{
    private int hp;
    public int Hp
    {
        get { return hp; }
        set
        {
            hp = value;
            if(hp<0)
            {
                Destroy(gameObject);
            }
        }

    }
    private void Start()
    {
        Hp = 100;
    }
    public void Hit(IAttackable target)
    {
        Hp -= target.Atk;
    }

    
}
