using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float sensX, sensY;
    public Transform orientation;
    float xRot, yRot;

    void Start()
    {
        //Cursor Lock Mode (None, Locked, Confined)
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    private void Update()
    {
        //Time.deltaTime used to make something regardless of the frames in update.
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;
        yRot += mouseX;
        xRot -= mouseY;
        //Mathf.Clamp is clamping the camera to minimum and maximum of rotation or movement.
        xRot = Mathf.Clamp(xRot, -90f, 90f);
        //tranform can be used in any gameobjects and getting it's transform place and rotation.
        //Quaternion.Euler is used to rotate an object to x, y, z, vector.
        transform.rotation = Quaternion.Euler(xRot, yRot, 0);
        orientation.rotation = Quaternion.Euler(0, yRot, 0);
    }
}
