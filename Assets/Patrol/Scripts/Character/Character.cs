using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour,IHitable
{
    [Header("캐릭터 속성")]
    [SerializeField] protected int hp;
    [SerializeField] protected int atk;
    public int maxHp = 100;
    [SerializeField] protected float speed;
    
    public  int Hp 
    {   
        get { return hp; } 
        set 
        {
            hp = value;
            if(hp <= 0)
            {
                Die();
            }
            if(hp > maxHp)
            {
                hp = maxHp;
            }
        } 
    }

    public abstract int Atk { get; set; }

    public abstract void Die();

    public abstract void Hit(IAttackable target);

}
