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
    public bool setWeaponBool = false;
    private Animator animator;

    public Transform weaponSlot; 
    public GameObject[] weapons; 
    public GameObject weaponObj; 
    public Weapon weapon;

    List<GunAttackStartegy> gunAttackList = new List<GunAttackStartegy>();
    public  Weapon_Type weaponType;

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
        gunAttackList.Add(new PistolAttackStartegy(this));
        invenWindowObj.SetActive(false);
    }

    private void Update()
    {
        if(!setWeaponBool)
            SetWeapon();
        if (!stop)
        {
            Move();
            Attack();
        }
        if(Input.GetKeyDown(KeyCode.Alpha1))            //무기를 교체하는 부분
        {
            Debug.Log("1번 통과");
            ChangeWeapon(weapons[0]);

        }
        if(Input.GetKeyDown(KeyCode.Alpha2)) 
        {
            if (weapons[1] == null)
                Debug.Log("샷건이 없습니다");

            else
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
        weaponType = weaponObj.GetComponent<Weapon>().weapon_Type;
        Debug.Log((int)weaponType);
        if(weaponType == Weapon_Type.PISTOL)
        {
            pistolImage.gameObject.SetActive(true);
            shotgunImage.gameObject.SetActive(false);
        }
        else if(weaponType == Weapon_Type.SHOTGUN)
        {
            pistolImage.gameObject.SetActive(false);
            shotgunImage.gameObject.SetActive(true);
        }
    }
    public void ChangeWeapon(GameObject weaponObj)
    {
        for(int i = 0; i < weapons.Length; i++) 
        {
            Debug.Log(i);
            if (weapons[i] == weaponObj)
            {
                weaponObj.SetActive(true);
                weapon = weaponObj.GetComponent<Weapon>();
                ChangeImageWeapon(weaponObj);
            }
            else
            {
                if (weapons[i] == null)
                    Debug.Log("무기가 없습니다");
                else
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
        gunAttackList.Add(new ShotgunAttackStartegy(this));
        weaponType = Weapon_Type.SHOTGUN;
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
            if (this.weaponType == Weapon_Type.PISTOL)
                GunFireStart(this.weaponType, "PistolAiming", "PistolShoot");
            else if (this.weaponType == Weapon_Type.SHOTGUN)
                GunFireStart(this.weaponType, "PistolAiming", "PistolShoot");
        }
        else
            GunFireEnd(this.weaponType, "PistolAiming");
    }
    public void GunFireStart(Weapon_Type weaponType, string aimStartAniName, string shootAniName)
    {
        if (this.weaponType == weaponType)
        {
            animator.SetBool(aimStartAniName, true);
            aiming = true;
            if (Input.GetKeyDown(KeyCode.Mouse0) && weapon.BulletCount >0)
            {
                if (this.weaponType == Weapon_Type.PISTOL)
                    SoundManager.instance.Play(pistolAudio, transform, false);
                else if (this.weaponType == Weapon_Type.SHOTGUN)
                    SoundManager.instance.Play(shotgunAudio, transform, false);
                animator.SetTrigger(shootAniName);
            }
        }
    }
    public void GunFireEnd(Weapon_Type weaponType, string aimEndAniName)
    {
        if(this.weaponType == weaponType)
        {
            aiming = false;
            animator.SetBool(aimEndAniName, false);
        }
    }
   
    public void GunShoot()
    {
        Shoot(true, weaponType);
    }
    public void ShotgunShoot()
    {
        Shoot(true, weaponType);
    }
    public void Shoot(bool oneShoot, Weapon_Type weapon_Type)
    {
        weapon.fire = oneShoot;
        if (weapon.fire)
        {
            gunAttackList[(int)weapon_Type].ShootGun();
            weapon.fire = !oneShoot;
        }
    }

    public void SetWeapon()
    {
        CursorSwitch(true);
        GameObject setWeapon = Instantiate(weapons[0], weaponSlot);
        weapons[0] = setWeapon;
        inventoryUI.AddItem(setWeapon.GetComponent<Item>());
        weapon = weapons[0].GetComponent<Weapon>();
        setWeaponBool = true;
        pistolImage.enabled = true;
        weapons[0].GetComponent<Weapon>().weapon_Type = Weapon_Type.PISTOL;
    }
    
    public void StairsMove()
    {
        //float maxDistance = 100;
        //RaycastHit hitY;
        //RaycastHit hitZ;
        //Debug.DrawLine(transform.position, transform.forward * maxDistance, Color.red);
        //Debug.DrawLine(transform.position, new Vector3(0, 1, 1) * maxDistance, Color.blue);
        //if (Physics.Raycast(transform.position, transform.forward,out hitY, maxDistance))
        //{
        //    Debug.Log("쏜다!");

        //}
    } //아직 미구현
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


