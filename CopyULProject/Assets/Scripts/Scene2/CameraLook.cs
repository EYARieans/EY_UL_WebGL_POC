using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraLook : MonoBehaviour
{
    //for camera change position
    public Transform pos1;
    public Transform pos2;
    public Transform pos3;//for 3rd room
    public GameObject Canvas_level2;//for 3rd room
    public GameObject Canvas_level3;//for 3rd room
    public GameObject Ripples;//for 3rd room
    public GameObject Grapple_Gun;//for 3rd room
    public Transform pos4;//for 3rd room
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
       // Dynamic_fov();
        

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
     public void Dynamic_fov()
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


    IEnumerator Pos_change_level3() //waiting for grapple gun to end its animation,grapple gun clicks : 1st canvas off ,2nd canvas on ,3rd camera positon change,4th ripples should be off
    {
        yield return new WaitForSeconds(2f);
        Canvas_level2.SetActive(false);
        Canvas_level3.SetActive(true);
        cam.transform.position = pos3.transform.position;
        Ripples.SetActive(false);
        Grapple_Gun.SetActive(false);
    }

    public void Level3()
    {
        StartCoroutine(Pos_change_level3());
        //SceneManager.LoadScene(1);
    }
    public void Pos_change_L3()
    {
        cam.transform.position = pos4.transform.position;
    }
    public void Pos_change_back_L3()
    {
        cam.transform.position = pos3.transform.position;
    }


}
