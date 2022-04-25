using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;
using TMPro;

public class PlayerMovement_Scr : MonoBehaviour
{
    float playerHeight = 2f;

    [SerializeField] Transform orientation;

    [Header("Movement")]
    
    [SerializeField] float moveSpeed = 6f;
    [SerializeField] float airMultiplier = 0.4f;
    float movementMultiplier = 10f;

    [Header("Sprinting")]
    [SerializeField] float walkSpeed = 6f;
    [SerializeField] float sprintSpeed = 10f;
    [SerializeField] float acceleration = 10f;

    [Header("Jumping")]
    [SerializeField] float jumpForce = 5f;

    [Header("Keybinds")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space; //can set as null and use a keybind manager
    [SerializeField] KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Drag and gravity")]
    [SerializeField] float groundDrag = 6f;
    [SerializeField] float airDrag = 2f;

    float horizontalMovement;
    float verticalMovement;

    [Header("Ground Detection")]
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float groundDistance = 0.4f;
    bool isGrounded;

    Vector3 moveDirection;
    Vector3 slopeMoveDirection;
    Vector3 jumpMoveDirection;

    bool isCrouching = false;
    bool isSliding = false;

    Rigidbody rb;

    RaycastHit slopeHit;

    //when called it will send a raycast out and return is true if the vector does not return stright up
    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight / 2 + 0.5f))
        {
            if (slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    public void Start()
    {
        //rigid body settings are set as well as varibles and the script get the rigidbody
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.drag = 0f;
    }


    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); //is grounded check


        //runs myinput funtion and controll drag (more over this farther down)
        MyInput();
        ControllDrag();
        ControlSpeed();

        //allows the player to jump if they are on the ground and presses the jump key
        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.C) && isGrounded && !isCrouching && Input.GetAxis("Vertical") > 0)
        {
            Slide();
        }
        else if (Input.GetKey(KeyCode.C) || Physics.Raycast(transform.position, Vector3.up, (playerHeight / 2) + 0.2f))
        {
            Crouch();
        }
        else
        {
            Stand();
        }

        //sets the direction following the slope
        slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);

        //moive direction so the player can control inair movement
        jumpMoveDirection = moveDirection * 0.1f;

        //gets the player's height by getting the scale of the Rigidbody and timesing by 2 as defult is 1
        playerHeight = rb.transform.localScale.y * 2;
        groundDistance = rb.transform.localScale.y * 2 / 5;
    }

    //when called it will grab movement input
    void MyInput()
    {
        //grabs key inputs
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        //combines both inputs into one direction
        moveDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement;
    }

    void Jump() //when called then the player will jump in the air
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse); //add a jump force to the rigid body component.
    }

    void Crouch()
    {
        isCrouching = true;
        rb.transform.localScale = new Vector3(1f, 0.5f, 1f); // sets to crouch size
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundCheck.position, groundDistance);
    }

    void Slide()
    {
        if (!isSliding)
        {
            rb.transform.localScale = new Vector3(1f, 0.5f, 1f);
            rb.AddForce(moveDirection.normalized * sprintSpeed * 30f, ForceMode.Impulse);
        }
        isSliding = true;
    }

    void Stand()
    {
        isCrouching = false;
        rb.transform.localScale = Vector3.one; // sets size back to normal
    }

    void ControlSpeed()
    {
        if (Input.GetKey(sprintKey) && Input.GetAxis("Vertical") > 0 && isGrounded && !isCrouching)
        {
            moveSpeed = Mathf.Lerp(moveSpeed, sprintSpeed, acceleration * Time.deltaTime);
        }
        else if (isSliding)
        {
            if (rb.velocity.magnitude > 2)
            {
                isSliding = true;
            }
            else
            {
                isSliding = false;
                isCrouching = true;
            }
        }
        else if (isCrouching)
        {
            moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed - 0.35f, acceleration * Time.deltaTime);
        }
        else
        {
            moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, acceleration * Time.deltaTime);
        }
    }

    //when called then drag will be controlled
    void ControllDrag()
    {
        //drag is controlled if in air or ground so the player is not slow falling
        if (isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = airDrag;
        }
    }

    private void FixedUpdate()
    {
        //calls the Move player funtion
        MovePlayer();
    }

    //when called then the rigid body will get force applyed
    void MovePlayer()
    {
        //if on the ground but not on a slope
        if (isGrounded && !OnSlope())
        {
            //walk speed on flat ground
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }

        //if the player is on the ground and on a slope
        else if (isGrounded && OnSlope())
        {
            //walk speed on slope
            rb.AddForce(slopeMoveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }

        //if the player is not on the ground
        else if (!isGrounded)
        {
            //jumping in mid air force with a downwards force
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier * airMultiplier, ForceMode.Acceleration);
            rb.useGravity = true;
        }
    }
}
