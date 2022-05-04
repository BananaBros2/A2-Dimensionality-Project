using UnityEngine;

public class BasicPlayerMovementController : MonoBehaviour
{
    private float playerHeight = 0f;

    public Transform orientation;
    public Transform headPosition;
    public Transform cameraPosition;
    public WallRunController wallRunController;
    public Rigidbody rb;

    [Header("Keybinds")]
    public KeyCode jumpKey;
    public KeyCode walkKey;
    public KeyCode scaleUpKey;
    public KeyCode scaleDownKey;

    [Header("Basic Movement")]
    public float airMultiplier;

    public bool IsMoving { get; private set; } = false;
    public bool IsGrounded { get; private set; } = false;
    public bool IsCrouching { get; private set; } = false;

    private float horizontalMovement = 0f;
    private float verticalMovement = 0f;
    private float currentMovementSpeed = 0f;
    private float movementMultiplier = 10f;

    [Header("Sprinting")]
    public float walkSpeed;
    public float sprintSpeed;
    public float acceleration;

    [Header("Jumping")]
    public float jumpForce;

    [Header("Drag and Gravity")]
    public float groundDrag;
    public float airDrag;

    [Header("Ground Detection")]
    public Transform groundCheck;
    public LayerMask groundMask;
    public float groundDistance;

    private Vector3 moveDirection = Vector3.zero;
    private Vector3 slopeMoveDirection = Vector3.zero;
    private RaycastHit slopeHit;
    //private Vector3 jumpMoveDirection; May need this in the future

    private void Update()
    {
        IsGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); //is grounded check

        //runs myinput function and control drag (more over this further down)
        SetMoveDirectionFromInput();
        ControlDrag();
        ControlSpeed();

        //allows the player to jump if they are on the ground and presses the jump key
        if (Input.GetKeyDown(jumpKey) && IsGrounded)
        {
            Jump();
        }

        if (Input.GetKey(KeyCode.C))
        {
            Crouch();
        }
        else
        {
            Stand();
        }

        //sets the direction following the slope
        slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);

        //move direction so the player can control inair movement
        //jumpMoveDirection = moveDirection * 0.1f;

        //gets the player's height by getting the scale of the Rigidbody and timesing by 2 as defult is 1
        playerHeight = transform.localScale.y * 2;
        groundDistance = playerHeight / 5;
    }

    private void FixedUpdate()
    {
        //calls the Move player funtion
        MovePlayer();
    }

    //when called it will grab movement input
    void SetMoveDirectionFromInput()
    {
        //grabs key inputs
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        //combines both inputs into one direction
        moveDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement;
    }

    //when called then drag will be controlled
    void ControlDrag()
    {
        //drag is controlled if in air or ground so the player is not slow falling
        if (IsGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = airDrag;
        }
    }

    void ControlSpeed()
    {
        if (!wallRunController.isWallRunning || IsGrounded) //DO NOT TOUCH IF STATEMENT UNLESS YOU WANT TO REWORK BOTH THIS AND WALL RUNNING SCRIPTS
        {
            float ySpeed = rb.velocity.y;

            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

            // clamps to max speed
            if (rb.velocity.magnitude > sprintSpeed)
            {
                rb.velocity = rb.velocity.normalized * sprintSpeed;
            }

            rb.velocity = new Vector3(rb.velocity.x, ySpeed, rb.velocity.z);


            //if (isGrounded && Input.GetAxis("Vertical") > 0)
            //{
            //    if (Mathf.Abs(Input.GetAxis("Mouse X")) > 7)
            //    {
            //        moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, Mathf.Abs(Input.GetAxis("Mouse X")) * Time.deltaTime);
            //        print(Mathf.Abs(Input.GetAxis("Mouse X")));
            //    }
            //    else
            //    {
            //        moveSpeed = Mathf.Lerp(moveSpeed, sprintSpeed, Input.GetAxisRaw("Mouse X") * 2f * Time.deltaTime);
            //    }

            //    isMoving = true;
            //}

            if (IsGrounded && Input.GetAxis("Vertical") > 0)
            {
                currentMovementSpeed = Mathf.Lerp(currentMovementSpeed, sprintSpeed, acceleration * Time.deltaTime);

                IsMoving = true;
            }

            if (IsGrounded && Mathf.Abs(Input.GetAxis("Vertical")) > 0 && Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
            {
                currentMovementSpeed = Mathf.Lerp(currentMovementSpeed, walkSpeed, acceleration * Time.deltaTime);

                IsMoving = true;
            }

            else if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)
            {
                currentMovementSpeed = walkSpeed / 2;

                IsMoving = false;
            }
        }
        else if (wallRunController.isWallRunning)
        {
            currentMovementSpeed = walkSpeed * airDrag;
        }

        else if (IsCrouching)
        {
            currentMovementSpeed = Mathf.Lerp(currentMovementSpeed, walkSpeed / 2f, acceleration * Time.deltaTime);
        }
        else
        {
            currentMovementSpeed = Mathf.Lerp(currentMovementSpeed, walkSpeed, acceleration * Time.deltaTime);
        }
    }

    void Jump() //when called then the player will jump in the air
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse); //add a jump force to the rigid body component.
    }

    void Crouch()
    {
        IsCrouching = true;
        rb.transform.localScale = new Vector3(1f, 0.5f, 1f); // sets to crouch size
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundCheck.position, groundDistance);
    }

    void Stand()
    {
        IsCrouching = false;
        rb.transform.localScale = Vector3.one; // sets size back to normal
    }

    //when called it will send a raycast out and return is true if the vector does not return stright up
    private bool IsOnSlope()
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

    //when called then the rigid body will get force applyed
    void MovePlayer()
    {
        //if on the ground but not on a slope
        if (IsGrounded && !IsOnSlope())
        {
            //walk speed on flat ground
            rb.AddForce(moveDirection.normalized * currentMovementSpeed * movementMultiplier, ForceMode.Acceleration);
        }

        //if the player is on the ground and on a slope
        else if (IsGrounded && IsOnSlope())
        {
            //walk speed on slope
            rb.AddForce(slopeMoveDirection.normalized * currentMovementSpeed * movementMultiplier, ForceMode.Acceleration);
        }

        //if the player is not on the ground
        else if (!IsGrounded)
        {
            //jumping in mid air force with a downwards force
            rb.AddForce(moveDirection.normalized * currentMovementSpeed * airMultiplier, ForceMode.Acceleration);
        }
    }

    public float GetCurrentMovementSpeed()
    {
        return currentMovementSpeed;
    }

}
