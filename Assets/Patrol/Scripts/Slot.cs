using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    public InventoryUI playerInven;
    public Image image;
    public GameObject emptySprite;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI valueText;
    
    public Item item;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (item != null)
        {
            item.Use(playerInven.player);
            if (item is ConsumableItem)
            {
                if(((ConsumableItem)item).isUseable == false)
                    SetItem(null);
            }
            //  item.Use(player);
        }
        Debug.Log(gameObject.name + "클릭");
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(gameObject.name + "다운");
    }

    public void OnPointerUp(PointerEventData eventData)
    {

        Debug.Log(gameObject.name + "업");
        Slot targetSlot = eventData.pointerEnter.gameObject.GetComponent<Slot>();
        if (targetSlot != null)
        {
            Item tempItem = targetSlot.item;
            targetSlot.SetItem(item);
            SetItem(tempItem);
        }
    }
    public void SetItem(Item setItem)
    {
        item = setItem;
        if (item != null ) 
        {
            image.sprite = item.sprite;
            nameText.text = item.name;
            valueText.text = item.value +"";
        }
        emptySprite.SetActive(item == null);
    }
}
