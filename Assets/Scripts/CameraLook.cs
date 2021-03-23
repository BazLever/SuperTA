using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{

    public float mouseSensitivity = 100f;

    public Transform playerBody;

    float xRotation = 0f;

    float cameraPitch = 0;
    float cameraYaw = 0;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        //float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        //float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        //xRotation -= mouseY;
        //xRotation = Mathf.Clamp(xRotation, -85f, 85f);

        transform.localRotation = Quaternion.Euler(cameraYaw, 0f, 0f);
        playerBody.rotation = Quaternion.Euler(0, cameraPitch, 0); // (Vector3.up * cameraPitch * Mathf.Deg2Rad);
    }

    private void FixedUpdate()
    {
        cameraPitch += Input.GetAxis("Mouse X") * mouseSensitivity;
        cameraYaw -= Input.GetAxis("Mouse Y") * mouseSensitivity;

        cameraYaw = Mathf.Clamp(cameraYaw, -85f, 85f);
    }
}
