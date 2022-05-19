using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpPlayerCameraController : MonoBehaviour
{
    public Camera cam;
    public Transform neckPivot;

    [Header("Mouse Sensitivity")]
    public float horizontalSensitivity;
    public float verticalSensitivity;

    private float verticalClamp = 88f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCameraLookDirection();
    }

    private void UpdateCameraLookDirection()
    {
        float xAxisRotation = Input.GetAxisRaw("Mouse Y") * -verticalSensitivity * Time.deltaTime;
        float yAxisRotation = Input.GetAxisRaw("Mouse X") * horizontalSensitivity * Time.deltaTime;
        Vector3 eulerAngles;
        eulerAngles = neckPivot.localEulerAngles;
        eulerAngles.x = (eulerAngles.x > 180f) ? eulerAngles.x - 360f : eulerAngles.x;
        eulerAngles.x += xAxisRotation;
        eulerAngles.y += yAxisRotation;
        eulerAngles.z = 0f;
        eulerAngles.x = Mathf.Clamp(eulerAngles.x, -verticalClamp, verticalClamp);
        neckPivot.localRotation = Quaternion.Euler(eulerAngles);
    }
}
