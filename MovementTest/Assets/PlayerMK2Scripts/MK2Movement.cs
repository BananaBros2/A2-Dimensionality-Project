using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;
using TMPro;

public class MK2Movement : MonoBehaviour
{
    [SerializeField] Transform groundCheckLocation;
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] LayerMask groundMask;

    CharacterController cc;
    [SerializeField] float speed = 10f;

    [SerializeField] float gravity = -9.81f;

    Vector3 moveDirection;
    Vector3 velocity;

    bool isGrounded;

    [SerializeField] float jumpHeight = 3f;

    [Header("Wall Running settings")]
    bool wallLeft = false;
    bool wallRight = false;

    RaycastHit leftWallHit;
    RaycastHit rightWallHit;

    [SerializeField] float wallRunGravity;
    [SerializeField] float wallRunJumpForce;
    [SerializeField] float wallRunSpeed;

    [SerializeField] float wallDistance = .5f;
    [SerializeField] float minimumJumpHeight = 1.5f;

    bool UseGravity = true;

    private void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    private void Update()
    {
        BasicMovement();

        WallRun();



        if (isGrounded && Input.GetKey(KeyCode.C))
        {
            groundDistance = 1f;
            cc.transform.localScale = new Vector3(1f, 0.6f, 1f);
        }
        else
        {
            groundDistance = 0.4f;
            cc.transform.localScale = Vector3.one;
        }
        

    }

    //wall running

    void StopWallRun()
    {
        UseGravity = true;

        //cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, fov, wallRunfovTime * Time.deltaTime);
        //tilt = Mathf.Lerp(tilt, 0, camTiltTime * Time.deltaTime);
    }

    void StartWallRun()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (wallLeft)
            {
                Vector3 wallRunJumpDirection = transform.up + rightWallHit.normal; // This sets the jump direction to the tilt of the wall
                velocity.y = 1f; // this sets the y value to 1 so you can jump off the wall in the next line
                cc.Move(wallRunJumpDirection * wallRunJumpForce); // This apply all the forces collected
            }  
            else if (wallRight)
            {
                /*
                Vector3 wallRunJumpDirection = transform.up + rightWallHit.normal; // This sets the jump direction to the tilt of the wall
                velocity.y = 1f; // this sets the y value to 1 so you can jump off the wall in the next line

                //lerps between two values like a animation so you can see the player jump instead of it teleport (hard go to the position)
                float Y = Mathf.Lerp(cc.transform.localPosition.y, wallRunJumpDirection.y * wallRunJumpForce, 1f * Time.deltaTime);
                float X = Mathf.Lerp(cc.transform.localPosition.x, wallRunJumpDirection.x * wallRunJumpForce, 1f * Time.deltaTime);
                float Z = Mathf.Lerp(cc.transform.localPosition.z, wallRunJumpDirection.z * wallRunJumpForce, 1f * Time.deltaTime);

                cc.Move(new Vector3(X, Y, Z)); // wallRunJumpDirection * wallRunJumpForce); // This apply all the forces collected
                */

                cc.SimpleMove(new Vector3(1f, 1f, 1f));
            }
        }

        velocity.y += wallRunGravity * Time.deltaTime;

        UseGravity = false;

        //cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, wallRunfov, wallRunfovTime * Time.deltaTime);

        if (wallLeft)
            print("wall left");//tilt = Mathf.Lerp(tilt, -camTilt, camTiltTime * Time.deltaTime);
        else if (wallRight)
            print("wall Right");//tilt = Mathf.Lerp(tilt, camTilt, camTiltTime * Time.deltaTime);
    }

    void WallRun()
    {
        CheckWall();

        if (canWallRun())
        {
            if (wallLeft || wallRight)
            {
                StartWallRun();
            }
            else
            {
                StopWallRun();
            }
        }
        else
        {
            StopWallRun();
        }
    }

    bool canWallRun()
    {
        Debug.DrawRay(transform.position, Vector3.down, Color.green, minimumJumpHeight);
        return !Physics.Raycast(transform.position, Vector3.down, minimumJumpHeight);
    }

    void CheckWall()
    {
        wallLeft = Physics.Raycast(transform.position, -cc.transform.right, out leftWallHit, wallDistance);
        wallRight = Physics.Raycast(transform.position, cc.transform.right, out rightWallHit, wallDistance);
        Debug.DrawRay(transform.position, -cc.transform.right, Color.cyan, wallDistance);
        Debug.DrawRay(transform.position, cc.transform.right, Color.red, wallDistance);
    }


    //Movement
    void BasicMovement()
    {
        //Gravity Stuff
        if (UseGravity == true)
        {
            isGrounded = Physics.CheckSphere(groundCheckLocation.position, groundDistance, groundMask);

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -5f;
            }

            velocity.y += gravity * Time.deltaTime;
        }
        else
        {
            isGrounded = false;
        }

        cc.Move(velocity * Time.deltaTime);

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        moveDirection = transform.right * x + transform.forward * z;

        cc.Move(moveDirection.normalized * speed * Time.deltaTime);

        if (isGrounded && Input.GetKey(KeyCode.Space))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if (isGrounded && Input.GetAxis("Vertical") > 0 && Input.GetKey(KeyCode.LeftShift))
        {
            speed = Mathf.Lerp(speed, 20f, 5f * Time.deltaTime);
        }
        else
        {
            speed = Mathf.Lerp(speed, 10f, 5f * Time.deltaTime);
        }
    }
}
