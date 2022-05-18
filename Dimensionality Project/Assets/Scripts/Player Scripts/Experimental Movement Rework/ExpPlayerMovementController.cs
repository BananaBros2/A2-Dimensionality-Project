using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpPlayerMovementController : MonoBehaviour
{
    public ExpPlayerStateController stateController;
    public Transform facingDirection;
    public float maxSpeed;
    public float jumpForce;

    private Vector3 movementIntent = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMovementIntent();
    }

    private void UpdateMovementIntent()
    {
        switch (Input.GetAxisRaw("Forward/Backward Movement"))
        {
            case 1f: // forward
                movementIntent = Vector3.forward;
                break;

            case -1f: // backwards
                movementIntent = Vector3.back;
                break;

            case 0f: // no/both input(s)
                movementIntent = Vector3.zero;
                break;
        }
        switch (Input.GetAxisRaw("Strafe"))
        {
            case 1f: // right
                movementIntent += Vector3.right;
                break;

            case -1f: // left
                movementIntent += Vector3.left;
                break;

            case 0f: // no/both input(s)
                movementIntent += Vector3.zero;
                break;
        }
        movementIntent = movementIntent.normalized; // may or may not keep this line, depending on simplest implementation of UpdateMovement
        Debug.Log(movementIntent.ToString());
    }

    private void FixedUpdate()
    {
        UpdateMovement();
    }

    private void UpdateMovement()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
    }
}
