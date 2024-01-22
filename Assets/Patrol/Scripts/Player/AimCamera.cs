using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimCamera : MonoBehaviour
{
    private Player player;
    private bool boost = true;
    public int PriorityBoostAmount = 20;
    Cinemachine.CinemachineVirtualCameraBase vcam;

    void Start()
    {
        player = GetComponentInParent<Player>();
        vcam = GetComponent<Cinemachine.CinemachineVirtualCameraBase>();
    }
    void Update()
    {
        AimCam();
    }
    public void AimCam()
    {
        if (vcam != null)
        {
            if (player.aiming && boost)
            {
                vcam.Priority += PriorityBoostAmount;
                boost = false;
            }
            else if (!player.aiming && !boost)
            {
                vcam.Priority -= PriorityBoostAmount;
                boost = true;
            }
        }
    }
}
