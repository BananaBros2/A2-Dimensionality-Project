using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRunController : MonoBehaviour
{
    public bool isWallRunning { get; private set; } = false;

    [Header("Refernces")]
    public Transform orientation;
    public Camera cam;
    public Rigidbody rb;
    BasicPlayerMovementController movementScr;

    [Header("Wall Running checks")]
    public float wallDistance = .5f;
    public float minimumJumpHeight = 1.5f;
    public LayerMask wallMask;

    [Header("Wall Running forces")]
    public float wallRunGravity;
    public float wallRunJumpForce;
    public float wallRunSpeed;
    public float wallRunDesiredHeight;

    [Header("Camera settings")]
    public float fov;
    public float wallRunfov;
    public float wallRunfovTime;
    public float camTilt;
    public float camTiltTime;

    public float tilt { get; private set; }

    bool wallLeft = false;
    bool wallRight = false;

    RaycastHit leftWallHit;
    RaycastHit rightWallHit;

    private void Start()
    {
        movementScr = GetComponent<BasicPlayerMovementController>();
    }

    bool canWallRun() // runs a ground check
    {
        

        //if ( > 2f && !Physics.Raycast(transform.position, Vector3.down, minimumJumpHeight)) return true;
        //else return false;
        return !Physics.Raycast(transform.position, Vector3.down, minimumJumpHeight); // this returns the opposite of the raycast hit to see if your in mid air
    }

    void CheckWall() // checks what side the wall is on
    {
        wallLeft = Physics.Raycast(transform.position, -orientation.right, out leftWallHit, wallDistance, wallMask); // sends out a raycast to the left and sets left to ture if hit
        wallRight = Physics.Raycast(transform.position, orientation.right, out rightWallHit, wallDistance, wallMask); // sends out a raycast to the right and sets right to true if hit
    }

    void Update() // runs all wallrun functions every frame
    {
        WallRun(); // called the wall run manager
    }

    void WallRun() // this manages the wall running
    {
        CheckWall(); // calles the check wall to see what side the wall is on

        if (canWallRun()) // check if the player can wall run by runnung the check
        {
            if (wallLeft || wallRight) // checks if the player has a wall to their side
            {
                StartWallRun(); // if the player has a wall to the side they will begin to wall run
            }
            else // if not then they will not or stop wall running
            {
                StopWallRun(); // calles the stop wall running
            }
        }
        else // this stops the player from wall running when if they're touching the wall still
        {
            StopWallRun(); // calles the stop wall run function
        }
    }

    void StartWallRun() // the wall running script
    {
        if (!isWallRunning) rb.velocity = new Vector3(rb.velocity.x, 2f, rb.velocity.z);
        isWallRunning = true;

        if (Input.GetKeyDown(KeyCode.Space)) // checks if the player presses space
        {
            if (wallLeft) // if the wall is to the left and space is pressed
            {
                Vector3 wallRunJumpDirection = transform.up + leftWallHit.normal; // this will return the direction of the face of the wall
                rb.velocity = new Vector3(rb.velocity.x, wallRunDesiredHeight, rb.velocity.z); // will set y > 0 or it will make the player fall
                rb.AddForce(wallRunJumpDirection * wallRunJumpForce * 100, ForceMode.Force); // this applies the jump force (left and right)
            }
            else if (wallRight) // if the wall is to the right and space is pressed
            {
                Vector3 wallRunJumpDirection = transform.up + rightWallHit.normal; // this will return the direction of the face of the wall
                rb.velocity = new Vector3(rb.velocity.x, wallRunDesiredHeight, rb.velocity.z); // will set y > 0 or it will make the player fall
                rb.AddForce(wallRunJumpDirection * wallRunJumpForce * 100, ForceMode.Force); // this applies the jump force (left and right)
            }
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.S)) // checks if the player presses other movement so it will make the player fall
        {
            StopWallRun(); // calls the stop wall run fuction
        }

        rb.useGravity = false; // this stop normal gravity of pulling the player down at 9.81f allowing for custom gravity

        rb.AddForce(Vector3.down * wallRunGravity, ForceMode.Force); // this applies the custom gravity to the player

        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, wallRunfov, wallRunfovTime * Time.deltaTime); // this will lerp from the defult fov to the wall run fov over desired time

        if (wallLeft) // checks what side the wall is
            tilt = Mathf.Lerp(tilt, -camTilt, camTiltTime * Time.deltaTime); // this tilts the camera to the desired tilt opposite to the wall
        else if (wallRight) // checks what side the wall is
            tilt = Mathf.Lerp(tilt, camTilt, camTiltTime * Time.deltaTime); // this tilts the camera to the desired tilt opposite to the wall


        //rb.AddForce(orientation.forward * wallRunSpeed, ForceMode.Acceleration); // this makes the player move forward because if you stop you fall

    }

    void StopWallRun() // the stop wall run function
    {
        isWallRunning = false;

        rb.useGravity = true; // turns back on the normal gravity

        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, fov, wallRunfovTime * Time.deltaTime); // sets fov back to normal
        tilt = Mathf.Lerp(tilt, 0, camTiltTime * Time.deltaTime); // set the tilt back to normal
    }
}
