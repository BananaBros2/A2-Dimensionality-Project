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

    [Header("Jump Settings")]
    public float jumpForce;

    private bool[] inputArray = { false, false, false, false };
    private bool jumpIntent = false;
    private float minSpeed = 0.1f;

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

        if (!jumpIntent)
        {
            jumpIntent = Input.GetButtonDown("Jump");
        }
    }

    private void UpdateMovement()
    {
        if (stateController.isGrounded)
        {
            float accelToApply = 0f;
            float maxSpeedToApply = maxForwardSpeed;
            Vector3 directionToApply = Vector3.zero;

            string inputType = GetInputType();

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
                    maxSpeedToApply = (maxBackwardSpeed + maxStrafeSpeed) / 2;
                    directionToApply = Vector3.Normalize(-facingDirection.forward + -facingDirection.right);
                    break;

                case "Backward Right":
                    accelToApply = (backwardAccel + strafeAccel) / 2;
                    maxSpeedToApply = (maxBackwardSpeed + maxStrafeSpeed) / 2;
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
                    //maxSpeedToApply = rb.velocity.magnitude;
                    directionToApply = -rb.velocity.normalized;
                    break;
            }

            if (jumpIntent)
            {
                jumpIntent = false;
                Vector3 up = transform.up;
                Vector3 forward = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
                float t = (rb.velocity.magnitude / maxSpeedToApply) / 2f;
                float jumpX = Mathf.Lerp(up.x, forward.x, t);
                float jumpY = Mathf.Lerp(up.y, forward.y, t);
                float jumpZ = Mathf.Lerp(up.z, forward.z, t);
                Vector3 jumpDirection = new Vector3(jumpX, jumpY, jumpZ);
                Debug.Log("Up: " + up.ToString());
                Debug.Log("Forward: " + forward.ToString());
                Debug.Log("t: " + t.ToString());
                Debug.Log("Jump Direction: " + jumpDirection.ToString());
                rb.AddForce(jumpForce * transform.localScale.y * jumpDirection, ForceMode.VelocityChange);
            }

            rb.AddForce(accelToApply * transform.localScale.y * directionToApply, ForceMode.Acceleration);
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeedToApply);
        }

        if (Input.GetButton("Jump"))
        {

        }

        if (rb.velocity.magnitude < minSpeed) rb.velocity = Vector3.zero;
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
