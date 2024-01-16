using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public string name;
    public int value;
    public Sprite sprite;

    public abstract void Use(Player player);
}
