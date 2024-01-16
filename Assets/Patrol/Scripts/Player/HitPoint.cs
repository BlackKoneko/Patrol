using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPoint : MonoBehaviour, IAttackable
{
    private int atk;
    float maxLength;
    public LayerMask layerMask;
    public int Atk
    {
        get { return atk; }
        set { atk = value; }    
    }

    public void Attack(IHitable target)
    {
        target.Hit(this);
    }
    private void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * maxLength, Color.blue);
    }

    public void Hit(int Damage,float maxLength)
    {
        Debug.DrawRay(transform.position, transform.forward * maxLength, Color.red);
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, maxLength,layerMask) ) 
        {
            Debug.Log(hit.transform.gameObject + "¸Â¾Ò´Ù.");
            if (hit.transform.TryGetComponent<IHitable>(out IHitable target))
            {
                Atk = Damage;
                Debug.Log(Atk);
                Attack(target);
            }
        }

    }
}
