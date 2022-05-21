using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExpPlayerMovementController : MonoBehaviour
{
    public ExpPlayerStateController stateController;
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
    public float jumpForce;
    public int jumpForceRepetitions;

    private bool[] inputArray = { false, false, false, false };
    private bool jumpIntent = false;
    private bool jumpHang = false;
    private Vector3 jumpHangForce = -Physics.gravity / 4f;

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

        if (!jumpIntent && stateController.isGrounded)
        {
            jumpIntent = Input.GetButtonDown("Jump");
        }
        else if (!stateController.isGrounded)
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
        float accelToApply = 0f;
        float maxSpeedToApply = maxForwardSpeed;
        Vector3 directionToApply = Vector3.zero;

        string inputType = GetInputType();

        if (stateController.isGrounded) // on ground movement control
        {
            switch (inputType)
            {
                case "Forward":
                    accelToApply = forwardAccel;
                    maxSpeedToApply = maxForwardSpeed;
                    directionToApply = facingDirection.forward;
                    break;

                case "Backward":
                    accelToApply = backwardAccel;
                    maxSpeedToApply = maxBackwardSpeed;
                    directionToApply = -facingDirection.forward;
                    break;

                case "Left":
                    accelToApply = strafeAccel;
                    maxSpeedToApply = maxStrafeSpeed;
                    directionToApply = -facingDirection.right;
                    break;

                case "Right":
                    accelToApply = strafeAccel;
                    maxSpeedToApply = maxStrafeSpeed;
                    directionToApply = facingDirection.right;
                    break;

                case "Forward Left":
                    accelToApply = (forwardAccel + strafeAccel) / 2;
                    maxSpeedToApply = (maxForwardSpeed + maxStrafeSpeed) / 2;
                    directionToApply = Vector3.Normalize(facingDirection.forward + -facingDirection.right);
                    break;

                case "Forward Right":
                    accelToApply = (forwardAccel + strafeAccel) / 2;
                    maxSpeedToApply = (maxForwardSpeed + maxStrafeSpeed) / 2;
                    directionToApply = Vector3.Normalize(facingDirection.forward + facingDirection.right);
                    break;

                case "Backward Left":
                    accelToApply = (backwardAccel + strafeAccel) / 2;
                    maxSpeedToApply = maxBackwardSpeed;
                    directionToApply = Vector3.Normalize(-facingDirection.forward + -facingDirection.right);
                    break;

                case "Backward Right":
                    accelToApply = (backwardAccel + strafeAccel) / 2;
                    maxSpeedToApply = maxBackwardSpeed;
                    directionToApply = Vector3.Normalize(-facingDirection.forward + facingDirection.right);
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
                    directionToApply = (rb.velocity.magnitude > minSpeed) ? -rb.velocity.normalized : Vector3.zero;
                    break;
            }

            rb.AddForce(accelToApply * transform.localScale.y * directionToApply, ForceMode.Acceleration);
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeedToApply * transform.localScale.y);

            if (jumpIntent)
            {
                jumpIntent = false;
                StartCoroutine(PerformJump());
            }
        }
        else // in-air movement control
        {
            accelToApply = inAirAccel;
            switch (inputType)
            {
                case "Forward":
                    directionToApply = facingDirection.forward;
                    break;

                case "Backward":
                    directionToApply = -facingDirection.forward;
                    break;

                case "Left":
                    directionToApply = -facingDirection.right;
                    break;

                case "Right":
                    directionToApply = facingDirection.right;
                    break;

                case "Forward Left":
                    directionToApply = Vector3.Normalize(facingDirection.forward + -facingDirection.right);
                    break;

                case "Forward Right":
                    directionToApply = Vector3.Normalize(facingDirection.forward + facingDirection.right);
                    break;

                case "Backward Left":
                    directionToApply = Vector3.Normalize(-facingDirection.forward + -facingDirection.right);
                    break;

                case "Backward Right":
                    directionToApply = Vector3.Normalize(-facingDirection.forward + facingDirection.right);
                    break;

                case "Forward Backward":
                case "Left Right":
                case "Forward Backward Left":
                case "Forward Backward Right":
                case "Left Right Forward":
                case "Left Right Backward":
                case "All Inputs":
                case "No Input":
                    directionToApply = Vector3.zero;
                    break;
            }

            rb.AddForce(accelToApply * transform.localScale.y * directionToApply, ForceMode.Acceleration);

            if (jumpHang) // what happens when you hold jump in mid-air?
            {
                rb.AddForce(jumpHangForce * transform.localScale.y, ForceMode.Acceleration);
            }
        }
    }

    private IEnumerator PerformJump()
    {
        Vector3 up = transform.up;
        Vector3 forward = new Vector3(rb.velocity.x, 0f, rb.velocity.z).normalized;
        float t = (rb.velocity.magnitude / maxForwardSpeed) / 4f;
        Vector3 jumpDirection = Vector3.Lerp(up, forward, t);
        for (int applicationCount = 0; applicationCount < jumpForceRepetitions; applicationCount++)
        {
            rb.AddForce(jumpForce * transform.localScale.y * jumpDirection, ForceMode.Acceleration);
            yield return null;
        }
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
