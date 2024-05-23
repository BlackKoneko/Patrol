using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


[System.Serializable]
public class VerticalSlot
{
    public Slot[] slots = new Slot[3]; 
}

public class InventoryUI : MonoBehaviour
{
    public Player player;
    public VerticalSlot[] verticalSlots = new VerticalSlot[3];

    public void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                verticalSlots[i].slots[j].playerInven = this;
            }
        }
    }
    public void AddItem(Item item)
    {
        for(int i = 0; i<3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (verticalSlots[i].slots[j].item == null)
                {
                    verticalSlots[i].slots[j].SetItem(item);
                    return;
                }
            }
        }
    }
}
