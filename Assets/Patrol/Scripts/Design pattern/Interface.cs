using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public interface IHitable
{
    int Hp { get; set; }
    public void Hit(IAttackable target);
}

public interface IAttackable
{
    int Atk { get; }
    public void Attack(IHitable target);
}
