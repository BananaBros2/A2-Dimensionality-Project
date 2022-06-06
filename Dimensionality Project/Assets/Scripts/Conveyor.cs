using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MonoBehaviour
{
    public float power = 1f;
    public float flip = 1f;
    [HideInInspector]
    public Vector3 conveyorForce;
    void Start()
    {
        Animator Animator = GetComponent<Animator>();
        Animator.speed = power;
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            Rigidbody rb = other.GetComponentInParent<Rigidbody>();
            other.GetComponentInParent<BasicPlayerMovementController>().conveyorForce = -transform.right * power / 30 * flip;
        }
        else if (other.transform.tag == "RigidBody")
        {
            other.transform.Translate(-transform.right * power * Time.deltaTime * 1.75f * flip);
            if (other.attachedRigidbody.velocity.magnitude > power * flip)
            {
                other.attachedRigidbody.velocity = other.attachedRigidbody.velocity.normalized * power * flip;
            }
        }
    }
}
