using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour, IPointerClickHandler
{
    public Image mapButton;
    public Image settingButton;

    public void OnPointerClick(PointerEventData eventData)
    {
        eventData.pointerEnter.gameObject.GetComponent<Image>().color = Color.black;
        ButtonColor(Color.gray);
    }

    public void ButtonColor(Color color)
    {
        mapButton.color = color;
        settingButton.color = color;
    }
}
