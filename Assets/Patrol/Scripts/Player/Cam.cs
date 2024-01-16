using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    [SerializeField] private float camSpeed = 1.0f; //ķ�ӵ�
    private float yaw = 0; //Z ȸ����
    private float pitch = 0; //Y ȸ����

    private void Update()
    {
        yaw += camSpeed * Input.GetAxis("Mouse X"); //x���� ����
        pitch += camSpeed * Input.GetAxis("Mouse Y"); //y���� ����

        pitch = Mathf.Clamp(pitch, -45, 45);

        transform.eulerAngles = new Vector3(-pitch, yaw, 0.0f);

    }
}
