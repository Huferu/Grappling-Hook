using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float senxX;
    [SerializeField] private float senxY;
    [SerializeField] private Transform orientation;
    [SerializeField] private Transform cameraPos;
    private float xRotation;
    private float yRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        orientation.rotation = Quaternion.Euler(0, 0, 0);

    }

    // Update is called once per frame
    void Update()
    {
        /*if (gM.hasGameEnded)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            return;
        }*/

        transform.position = cameraPos.position;

        float mouseX = Input.GetAxisRaw("Mouse X") * senxX * MainMenu.mouseSensetivity * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * senxY * MainMenu.mouseSensetivity * Time.deltaTime;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
