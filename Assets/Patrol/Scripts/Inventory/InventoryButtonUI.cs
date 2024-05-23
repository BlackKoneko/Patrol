using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class InventoryButtonUI : MonoBehaviour
{
    public GameObject firstSeleteImage;
    public void FirstClick()
    {
        firstSeleteImage.SetActive(false);
    }

    public void Exit()
    {
        Debug.Log("메인메뉴로 이동");
    }
}
