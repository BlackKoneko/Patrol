using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Player : Character
{
    public int keynumber;

    public AudioClip pistolAudio;
    public AudioClip shotgunAudio;

    public GameObject setStatusUI;
    public GameObject invenWindowObj;
    public bool invenbutton = false;

    public InventoryUI inventoryUI;
    public GameObject invenObj;
    //public EquipmentItem equipmentItem;

    public Image pistolImage;
    public Image shotgunImage;
    public static Player player = null;
    public GameObject mainCamera;
    private Rigidbody rb;
    private float playerY;
    private Vector3 move;
    public bool stop = false;
    public bool aiming = false;
    private Animator animator;

    public Transform weaponSlot; 
    public GameObject[] weapons; 
    public GameObject weaponObj; 
    public Weapon weapon;


    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    public override int Atk { get { return atk; } set { atk = value; } }

    private void Start()
    {
        Hp = maxHp;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        invenWindowObj.SetActive(false);
        SetWeapon();
    }

    private void Update()
    {
        if (!stop)
        {
            Move();
            Attack();
        }
        if(Input.GetKeyDown(KeyCode.Alpha1))            //무기를 교체하는 부분
        {
            ChangeWeapon(weapons[0]);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2)) 
        {
            if (weapons[1] != null)
                ChangeWeapon(weapons[1]);
        }
        InvenWindow();        
    }
    public void CursorSwitch(bool cursorSwitch)
    {
        Cursor.visible = !cursorSwitch; //마우스를 안보이게 하는 부분
        if(cursorSwitch)
            Cursor.lockState = CursorLockMode.Locked;
        else
            Cursor.lockState = CursorLockMode.None;
    }
    public void ChangeImageWeapon(GameObject weaponObj) //
    {
        switch(weaponObj.GetComponent<Weapon>().weapon_Type)
        {
            case Weapon.Weapon_Type.PISTOL:
                pistolImage.gameObject.SetActive(true);
                shotgunImage.gameObject.SetActive(false);
                break;
            case Weapon.Weapon_Type.SHOTGUN:
                pistolImage.gameObject.SetActive(false);
                shotgunImage.gameObject.SetActive(true);
                break;
        }
    }
    public void ChangeWeapon(GameObject weaponObj)
    {
        for(int i = 0; i < weapons.Length; i++) 
        {
            if (weapons[i] == weaponObj)
            {
                weaponObj.SetActive(true);
                weapon = weaponObj.GetComponent<Weapon>();
                ChangeImageWeapon(weaponObj);
            }
            else
            {
                if (weapons[i] != null)
                    weapons[i].SetActive(false);
            }
        }
    }
    public GameObject AddWeapon(GameObject newWeapon)
    {
        GameObject addWeapon = Instantiate(newWeapon, weaponSlot);
        addWeapon.transform.position = weaponSlot.position;
        addWeapon.transform.eulerAngles = weaponSlot.eulerAngles;
        addWeapon.SetActive(false);
        for (int i = 0; i <= weapons.Length; i++)
        {
            if (weapons[i] == null)
            {
                weapons[i] = addWeapon;
                break;
            }
        }
        return addWeapon;
    }
    

    public void InvenWindow()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) 
        {
            if(!invenbutton)
            {
                CursorSwitch(false);
                Time.timeScale = 0;
                InvenSetting(invenbutton);
            }
            else
            {
                CursorSwitch(true);
                setStatusUI.SetActive(true);
                Time.timeScale = 1;
                InvenSetting(invenbutton);
            }

        }
    }
    public void InvenSetting(bool invenbutton)      //인벤토리 설정
    {
        stop = !invenbutton;                        
        invenWindowObj.SetActive(!invenbutton);
        this.invenbutton = !invenbutton;
    }
    private void OnTriggerStay(Collider other)
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            if(other.TryGetComponent<Item>(out Item item))
            {
                item.gameObject.SetActive(false);
                item.transform.SetParent(invenObj.transform);
                if(item is EquipmentItem) 
                {
                    inventoryUI.AddItem((AddWeapon(item.gameObject)).GetComponent<Item>());
                }
                else
                    inventoryUI.AddItem(item);
            }
            else if(other.TryGetComponent<Door>(out Door door))
            {
                door.Open();
            }
        }
    }
 
    public void Move()
    {
        move = Vector3.zero;
        playerY = rb.velocity.y;
              
        if (Input.GetKey(KeyCode.W))
        {
            move += transform.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            move -= transform.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            move -= transform.right;
        }
        if (Input.GetKey(KeyCode.D))
        {
            move += transform.right;
        }
        move = move.normalized * Speed * Time.deltaTime;
        move.y = playerY;
        rb.velocity = move;
        transform.eulerAngles = new Vector3(mainCamera.transform.eulerAngles.x, mainCamera.transform.eulerAngles.y, mainCamera.transform.eulerAngles.z);
    }
    public void Attack()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
             GunFireStart("PistolAiming", "PistolShoot");
        }
        else
            GunFireEnd("PistolAiming");
    }
    public void GunFireStart(string aimStartAniName, string shootAniName)
    {
         animator.SetBool(aimStartAniName, true);
         aiming = true;
         if (Input.GetKeyDown(KeyCode.Mouse0) && weapon.BulletCount >0)
         {
            weapon.Shoot();
            animator.SetTrigger(shootAniName);
         }
    }
    public void GunFireEnd(string aimEndAniName)
    {
         aiming = false;
         animator.SetBool(aimEndAniName, false);
    }

    public void SetWeapon()
    {
        CursorSwitch(true);
        GameObject setWeapon = Instantiate(weapons[0], weaponSlot);
        weapons[0] = setWeapon;
        inventoryUI.AddItem(setWeapon.GetComponent<Item>());
        weapon = weapons[0].GetComponent<Weapon>();
        pistolImage.enabled = true;
    }
    public override void Die()
    {
        CursorSwitch(false);
        SceneManager.LoadScene("Mainmenu");
    }
    
    public override void Hit(IAttackable target)
    {
        Hp -= target.Atk;
    }
}


