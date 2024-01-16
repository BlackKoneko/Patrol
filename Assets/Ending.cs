using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    public int endingNumber;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Player>(out Player player))
        {
            player.CursorSwitch(false);
            if (endingNumber == 1)
            {
                SceneManager.LoadScene("BadEnding");
            }
            else if (endingNumber == 2) 
            {
                SceneManager.LoadScene("Ending");
            }
        }
    }
}
