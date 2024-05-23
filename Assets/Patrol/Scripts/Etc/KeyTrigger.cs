using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class KeyTrigger : MonoBehaviour, IHitable
{
    [SerializeField]
    private KeyInstance KeyInstance;
    [SerializeField]
    private int triggerValue;

    private int hp;
    public int Hp
    {
        get { return hp; }
        set { hp = value; }
    }

    public void Hit(IAttackable target)
    {
        Debug.Log(triggerValue);
        KeyInstance.Enter("" + triggerValue);
    }
}
