using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;
using TMPro;

public class PlayerCamMove_Scr : MonoBehaviour
{
    [SerializeField] WallRun_Scr wallRun;

    [Header("Mouse sensitivity")]

    //old mouse sensitivity so you can set indivisual values to the mouse. Switch this out with the sensitivityMouse
    [SerializeField] private float sensX;
    [SerializeField] private float sensY;

    [SerializeField] float sensitivityMouse = 1; //DO NOT HARD SET THIS VALUE - USE A SCRIPT TO MANAGE SETTINGS

    [SerializeField] float multiplier = 0.01f;

    [Header("Mouse required objects")]
    //[SerializeField] Transform cam;
    Camera cam;
    [SerializeField] Transform orientation;

    float mouseX;
    float mouseY;

    float xRotation;
    float yRotation;

    private void Start()
    {
        cam = GetComponentInChildren<Camera>(); // this gets the camera component and sets cam 
        Cursor.lockState = CursorLockMode.Locked; // this locks the cursor
        Cursor.visible = false; // this hide the cursor
    }

    private void Update()
    {
        mouseX = Input.GetAxisRaw("Mouse X"); // this gets the X (left and right) mouse input
        mouseY = Input.GetAxisRaw("Mouse Y"); // this gets the Y (up and down) mouse input

        yRotation += mouseX * sensitivityMouse * multiplier; // this set the sensitvity and other values to the total mouse rotation
        xRotation -= mouseY * sensitivityMouse * multiplier; // this set the sensitvity and other values to the total mouse rotation

        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // this clamps the up down so you cannot flip the camera

        
        cam.transform.localRotation = Quaternion.Euler(xRotation, yRotation, wallRun.tilt); // this rotates the camera seperatly (up and down)
        orientation.transform.rotation = Quaternion.Euler(0, yRotation, 0); // this rotates the orientation (left and right)
    }
}
