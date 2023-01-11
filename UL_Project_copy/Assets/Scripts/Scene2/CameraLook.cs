//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraLook : MonoBehaviour
{
    //for camera change position
    public Transform pos1;
    public Transform pos2;
    public Camera cam;
    
   //for Camera Rotation
    public float sensX;
    public float sensY;
    float xRotation;
    float yRotation;
    public Button spyon;


    //for scrolling mouse
    float minFov = 30f;
    float maxFov =80f;
    float sens = 30f;

    // Update is called once per frame
    void Update()
    {
        MyInput();
        Dynamic_fov();
        

    }
    public  void MyInput()
    {
        if (Input.GetMouseButton(0))
        {
            float mouseX = Input.GetAxisRaw("Mouse X") * sensX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * sensY;

            yRotation += mouseX;
            xRotation -= mouseY;
            yRotation = Mathf.Clamp(yRotation, -180f, 180f);
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
           
            //rotate cam and orientation
            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        }
    }
    void Dynamic_fov()
    {
        float fov = cam.fieldOfView;
        fov -= Input.GetAxis("Mouse ScrollWheel") * sens;
        cam.fieldOfView = Mathf.Clamp(fov, minFov, maxFov);


    }
    public void Pos_change()
    {
        cam.transform.position = pos2.transform.position;
    }
    public void Pos_change_back()
    {
        cam.transform.position = pos1.transform.position;
    }
    
}
