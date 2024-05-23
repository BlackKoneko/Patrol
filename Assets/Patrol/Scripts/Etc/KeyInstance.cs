using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInstance : MonoBehaviour
{
    public GameObject key;
    public GameObject shotgun;
    public GameObject zombie;
    public string result;
    public string answer;
    int count = 0;
    public void Enter(string value)
    {
        result += value;
        count++;
        if (count >= 3)
        {
            if (answer.CompareTo(result) == 0)
            {
                Instantiate(key,transform.position,transform.rotation);
                Instantiate(shotgun, transform.position, transform.rotation);
            }
            else
            {
                Instantiate(zombie, transform.position, transform.rotation);
                count = 0;
                result = "";
            }
        }
    }
}
