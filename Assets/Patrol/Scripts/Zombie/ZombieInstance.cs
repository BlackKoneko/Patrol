using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieInstance : MonoBehaviour
{
    public GameObject InstanceZombie;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<Player>(out Player player))
        {
            InstanceZombie.SetActive(true);
        }
    }
}
