using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    [SerializeField] private float camSpeed = 1.0f; //캠속도
    private float yaw = 0; //Z 회전축
    private float pitch = 0; //Y 회전축

    private void Update()
    {
        yaw += camSpeed * Input.GetAxis("Mouse X"); //x값을 받음
        pitch += camSpeed * Input.GetAxis("Mouse Y"); //y값을 받음

        pitch = Mathf.Clamp(pitch, -45, 45);

        transform.eulerAngles = new Vector3(-pitch, yaw, 0.0f);

    }
}
