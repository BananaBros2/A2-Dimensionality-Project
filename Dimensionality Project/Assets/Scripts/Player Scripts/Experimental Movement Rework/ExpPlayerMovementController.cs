using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExpPlayerMovementController : MonoBehaviour
{
    public ExpPlayerStateController state;
    public Transform facingDirection;
    public Rigidbody rb;

    [Header("Max Speeds")]
    public float maxForwardSpeed;
    public float maxBackwardSpeed;
    public float maxStrafeSpeed;

    [Header("Acceleration Settings")]
    public float forwardAccel;
    public float backwardAccel;
    public float strafeAccel;
    public float inAirAccel;

    [Header("Jump Settings")]
    public float maxJumpHeight;

    private bool[] inputArray = { false, false, false, false };
    private string inputType = "No Input";

    private int jumpForceRepetitions = 5;
    private bool jumpIntent = false;
    private bool jumpHang = false;

    private const float minSpeed = 0.01f;

    private Dictionary<bool[], string> inputDictionary;

    // Start is called before the first frame update
    void Start()
    {
        InitialiseInputDictionary();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMovementIntent();
    }


    private void FixedUpdate()
    {
        UpdateMovement();
    }

    private void UpdateMovementIntent()
    {
        inputArray = GetInputThisFrame();

        if (!jumpIntent && state.isGrounded)
        {
            jumpIntent = Input.GetButtonDown("Jump");
        }
        else if (!state.isGrounded)
        {
            jumpHang = Input.GetButton("Jump");
        }
        else
        {
            jumpHang = false;
        }
    }

    private void UpdateMovement()
    {
        inputType = GetInputType();

        float accelToApply = 0f;
        float maxSpeedToApply = maxForwardSpeed;
        Vector3 directionToApply = GetDirectionToApply();

        if (state.isGrounded) // on ground movement control
        {
            switch (inputType)
            {
                case "Forward":
                    accelToApply = forwardAccel;
                    maxSpeedToApply = maxForwardSpeed;
                    break;

                case "Backward":
                    accelToApply = backwardAccel;
                    maxSpeedToApply = maxBackwardSpeed;
                    break;

                case "Left":
                    accelToApply = strafeAccel;
                    maxSpeedToApply = maxStrafeSpeed;
                    break;

                case "Right":
                    accelToApply = strafeAccel;
                    maxSpeedToApply = maxStrafeSpeed;
                    break;

                case "Forward Left":
                    accelToApply = (forwardAccel + strafeAccel) / 2;
                    maxSpeedToApply = (maxForwardSpeed + maxStrafeSpeed) / 2;
                    break;

                case "Forward Right":
                    accelToApply = (forwardAccel + strafeAccel) / 2;
                    maxSpeedToApply = (maxForwardSpeed + maxStrafeSpeed) / 2;
                    break;

                case "Backward Left":
                    accelToApply = (backwardAccel + strafeAccel) / 2;
                    maxSpeedToApply = maxBackwardSpeed;
                    break;

                case "Backward Right":
                    accelToApply = (backwardAccel + strafeAccel) / 2;
                    maxSpeedToApply = maxBackwardSpeed;
                    break;

                case "Forward Backward":
                case "Left Right":
                case "Forward Backward Left":
                case "Forward Backward Right":
                case "Left Right Forward":
                case "Left Right Backward":
                case "All Inputs":
                case "No Input":
                    accelToApply = backwardAccel / 2f;
                    break;
            }

            rb.AddForce(accelToApply * transform.localScale.y * directionToApply, ForceMode.Acceleration);
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeedToApply * transform.localScale.y);

            if (jumpIntent)
            {
                jumpIntent = false;
                PerformJump();
            }
        }
        else // in-air movement control
        {
            accelToApply = inAirAccel;

            rb.AddForce(accelToApply * transform.localScale.y * directionToApply, ForceMode.Acceleration);

            if (jumpHang) // what happens when you hold jump in mid-air?
            {
                
            }
        }
    }

    private Vector3 GetDirectionToApply()
    {
        Vector3 directionToApply = Vector3.zero;

        switch (inputType)
        {
            case "Forward":
                directionToApply = FlattenVector(facingDirection.forward).normalized;
                break;

            case "Backward":
                directionToApply = FlattenVector(-facingDirection.forward).normalized;
                break;

            case "Left":
                directionToApply = FlattenVector(-facingDirection.right).normalized;
                break;

            case "Right":
                directionToApply = FlattenVector(facingDirection.right).normalized;
                break;

            case "Forward Left":
                directionToApply = FlattenVector(facingDirection.forward + -facingDirection.right).normalized;
                break;

            case "Forward Right":
                directionToApply = FlattenVector(facingDirection.forward + facingDirection.right).normalized;
                break;

            case "Backward Left":
                directionToApply = FlattenVector(-facingDirection.forward + -facingDirection.right).normalized;
                break;

            case "Backward Right":
                directionToApply = FlattenVector(-facingDirection.forward + facingDirection.right).normalized;
                break;

            case "Forward Backward":
            case "Left Right":
            case "Forward Backward Left":
            case "Forward Backward Right":
            case "Left Right Forward":
            case "Left Right Backward":
            case "All Inputs":
            case "No Input":
                directionToApply = (!state.isGrounded || rb.velocity.magnitude < minSpeed) ? Vector3.zero : -rb.velocity.normalized;
                break;
        }
        return directionToApply;
    }

    private Vector3 FlattenVector(Vector3 vector)
    {
        return new Vector3(vector.x, 0f, vector.z);
    }

    private void PerformJump()
    {
        CalculateDistanceToJumpPeak();
        SmoothlyReachInitialVelocity(); // 3-5 frames

    }

    private void CalculateDistanceToJumpPeak()
    {

    }

    private void SmoothlyReachInitialVelocity()
    {

    }

    private string GetInputType()
    {
        string inputType = "";
        for (int keyIndex = 0; keyIndex < inputDictionary.Count; keyIndex++)
        {
            if (inputDictionary.Keys.ElementAt(keyIndex).SequenceEqual(inputArray))
            {
                inputDictionary.TryGetValue(inputDictionary.Keys.ElementAt(keyIndex), out inputType);
                return inputType;
            }
        }
        return inputType;
    }

    private bool[] GetInputThisFrame()
    {
        bool[] inputThisFrame = new bool[4];
        inputThisFrame[0] = Input.GetButtonDown("Strafe Left") || Input.GetButton("Strafe Left");
        inputThisFrame[1] = Input.GetButtonDown("Forward Movement") || Input.GetButton("Forward Movement");
        inputThisFrame[2] = Input.GetButtonDown("Backward Movement") || Input.GetButton("Backward Movement");
        inputThisFrame[3] = Input.GetButtonDown("Strafe Right") || Input.GetButton("Strafe Right");
        return inputThisFrame;
    }

    private bool GetJumpInput()
    {
        return Input.GetButtonDown("Jump") || Input.GetButton("Jump");
    }

    private void InitialiseInputDictionary() // the digits are ordered: left, forward, backward, right
    {
        inputDictionary = new Dictionary<bool[], string>();
        inputDictionary.Add(new bool[] { false, false, false, false }, "No Input");
        inputDictionary.Add(new bool[] { false, true, false, false }, "Forward");
        inputDictionary.Add(new bool[] { false, false, true, false }, "Backward");
        inputDictionary.Add(new bool[] { true, false, false, false }, "Left");
        inputDictionary.Add(new bool[] { false, false, false, true }, "Right");
        inputDictionary.Add(new bool[] { true, true, false, false }, "Forward Left");
        inputDictionary.Add(new bool[] { false, true, false, true }, "Forward Right");
        inputDictionary.Add(new bool[] { true, false, true, false }, "Backward Left");
        inputDictionary.Add(new bool[] { false, false, true, true }, "Backward Right");
        inputDictionary.Add(new bool[] { false, true, true, false }, "Forward Backward");
        inputDictionary.Add(new bool[] { true, false, false, true }, "Left Right");
        inputDictionary.Add(new bool[] { true, true, true, false }, "Forward Backward Left");
        inputDictionary.Add(new bool[] { false, true, true, true }, "Forward Backward Right");
        inputDictionary.Add(new bool[] { true, true, false, true }, "Left Right Forward");
        inputDictionary.Add(new bool[] { true, false, true, true }, "Left Right Backward");
        inputDictionary.Add(new bool[] { true, true, true, true }, "All Inputs");
    }
}
