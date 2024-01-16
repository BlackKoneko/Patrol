using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject plObj;
    private Player player;
    public Image HpBar;
    private Image hpImage;
    public TextMeshProUGUI pistolBulletText;
    public TextMeshProUGUI shotgunBulletText;
    private Weapon pistolWeapon;
    private Weapon shotgunWeapon;

    private void Start()
    {
        hpImage = HpBar.GetComponent<Image>();
        player = plObj.GetComponent<Player>();
    }
    private void Update()
    {
        HpStatus();
        BulletStatus();
    }
    public void HpStatus()
    {
        hpImage.fillAmount = Mathf.Lerp(HpBar.GetComponent<Image>().fillAmount, ((float)player.Hp / (float)player.maxHp), 3.0f * Time.deltaTime);
        if (player.Hp > 70)
        {
            hpImage.color = Color.green;
        }
        else if (player.Hp > 30)
        {
            hpImage.color = Color.yellow;
        }
        else
        {
            hpImage.color = Color.red;
        }
    }
    public void BulletStatus()
    {
        switch (player.weaponType)
        {
            case Weapon_Type.PISTOL:
                pistolWeapon = player.weapons[0].GetComponent<Weapon>();
                pistolBulletText.text = pistolWeapon.BulletCount + "/" + pistolWeapon.bulletCountMax;
                break;
            case Weapon_Type.SHOTGUN:
                shotgunWeapon = player.weapons[1].GetComponent<Weapon>();
                shotgunBulletText.text = shotgunWeapon.BulletCount + "/" + shotgunWeapon.bulletCountMax;
                break;
        }
    }
}
