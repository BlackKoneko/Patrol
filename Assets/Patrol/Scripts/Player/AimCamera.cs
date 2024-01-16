using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimCamera : MonoBehaviour
{
    private Player player;
    private bool boost = true;
    public int PriorityBoostAmount = 20;
    Cinemachine.CinemachineVirtualCameraBase vcam;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<Player>();
        vcam = GetComponent<Cinemachine.CinemachineVirtualCameraBase>();
    }

    // Update is called once per frame
    void Update()
    {
        if (vcam != null)
        {
            if (player.aiming && boost)
            {
                vcam.Priority += PriorityBoostAmount;
                boost = false;
            }
            else if(!player.aiming && !boost)
            {
                vcam.Priority -= PriorityBoostAmount;
                boost = true;
            }
        }
    }
}
