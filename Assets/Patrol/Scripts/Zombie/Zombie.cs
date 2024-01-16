using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.UIElements;

public class Zombie : Character, IAttackable, IHitable
{
    public AudioClip idleAudio;
    public AudioClip biteAudio;
    public bool switchIdleAudio = true;
    public bool switchBiteAudio = true;
    Collider collider;
    Rigidbody rb;
    private bool detect = false;
    [Header("탐지 범위")]
    public float radius;
    public float maxDistance;
    public LayerMask targetLayerMask;
    Animator animator;
    private bool bite = false;
    private bool biteAtk = false;
    private bool dying = false;

    public Player triggerInPlayer = null;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider>();
        Hp = maxHp;
        Atk = 20;
    }
    public override int Atk
    {
        get { return atk;}
        set { atk = value; }
    }
    public float Speed 
    {
        get { return speed;}
        set { speed = value;}
    }
    private void Update()
    {
        Detection();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent<IHitable>(out IHitable target))
        {
            if (!bite)
            {
                if(switchBiteAudio)
                {
                    SoundManager.instance.Play(biteAudio, transform, false);
                    switchBiteAudio= false;
                }
                animator.SetTrigger("Bite");
            }
            if (biteAtk)
            {
                triggerInPlayer.Hit(this);
                biteAtk = false;
            }

            animator.SetBool("Walk", false);
            rb.velocity = Vector3.zero;

            triggerInPlayer = GameManager.instance.player;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<IHitable>(out IHitable player))
        {
            triggerInPlayer.stop = false;
            triggerInPlayer = null;
        }
    }


    public void Detection()
    {
        RaycastHit hit;
        Collider[] cols = null;      
        cols = Physics.OverlapSphere(transform.position, radius, targetLayerMask);

        if (cols.Length > 0)
        {
             Vector3 direction = ((cols[0].transform.position) - transform.position).normalized;
            if (Physics.Raycast(transform.position, direction, out hit, maxDistance))
            {
                Debug.DrawLine(transform.position, transform.position + (direction * maxDistance), Color.yellow);
                if (cols[0].gameObject.layer == hit.transform.gameObject.layer)
                {
                    transform.forward = hit.point-transform.position;
                    if (!bite && !dying)
                    {
                        if(switchIdleAudio)
                        {
                            switchIdleAudio = false;
                            SoundManager.instance.Play(idleAudio, this.transform, false);
                        }
                        animator.SetBool("Walk", true);
                        rb.velocity = direction.normalized * Speed * Time.deltaTime;
                    }
                }
            }
        }
        else 
        {
            switchIdleAudio = true;
            animator.SetBool("Walk", false);
            rb.velocity = Vector3.zero;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red; 
        Gizmos.DrawWireSphere(transform.position, radius); //원으로 그려주는 것
    }
    public void BiteStart()
    {
        //playerStop = true;
        if(triggerInPlayer != null)
            triggerInPlayer.stop = true;
        bite = true;
        Debug.Log("물어 뜯기 시작");
    }
    public void BiteEnd()
    {
        switchBiteAudio = true;
        bite = false;
        Debug.Log("물어 뜯기 끝");
    }
    public void BitePosStart()
    {
        biteAtk = true;
    }
    public void BitePosEnd()
    {
        if (triggerInPlayer != null)
            triggerInPlayer.stop = false;
        biteAtk = false;
    }
    IEnumerator Dying()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
    public override void Die()
    {
        collider.enabled = false;
        dying = true;   
        if(dying)
            rb.velocity = Vector3.zero;
        animator.SetBool("Dying", true);
        StartCoroutine(Dying());
    }


    public override void Hit(IAttackable target)
    {
        Hp -= target.Atk;
    }

    public void Attack(IHitable target)
    {
        target.Hp -= Atk;
    }
}
