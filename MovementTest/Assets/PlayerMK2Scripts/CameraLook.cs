using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    float x;
    float y;

    float xRoation;
    float yRoation;

    [SerializeField] float sens = 100f;

    [SerializeField] Camera cam;

    [SerializeField] Transform body;

    void Start()
    {
        cam = GetComponentInChildren<Camera>();
    }

    void Update()
    {
        x = Input.GetAxisRaw("Mouse X");
        y = Input.GetAxisRaw("Mouse Y");

        xRoation += x * sens;
        yRoation -= y * sens;

        yRoation = Mathf.Clamp(yRoation, -90f, 90f);

        cam.transform.localRotation = Quaternion.Euler(yRoation, 0, 0);
        body.transform.rotation = Quaternion.Euler(0, xRoation, 0);
    }
}
