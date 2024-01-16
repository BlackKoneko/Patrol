using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Player player;
    public int openKeyNum;
    public void Open()
    {
        if (player.keynumber >= openKeyNum)
        {
            transform.eulerAngles = new Vector3(0, 90, 0);
        }
    }
}
