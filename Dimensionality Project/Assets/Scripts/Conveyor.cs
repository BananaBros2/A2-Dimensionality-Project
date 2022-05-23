using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MonoBehaviour
{
    public float power = 1f;
    [HideInInspector]
    public Vector3 conveyorForce;
    public bool speedParameterActive;
    [SerializeField] Animator mainAnimator;

    private void OnTriggerStay(Collider other)
    {
        mainAnimator = GetComponent<Animator>();
        mainAnimator.SetFloat("speed", power);

        if (other.transform.tag == "Player")
        {
            Rigidbody rb = other.GetComponentInParent<Rigidbody>();
            BasicPlayerMovementController BPMS = other.GetComponentInParent<BasicPlayerMovementController>();
            other.GetComponentInParent<BasicPlayerMovementController>().conveyorForce = -transform.right * power / 30;
        }
        else if (other.transform.tag == "RigidBody")
        {
            other.transform.Translate(-transform.right * power * Time.deltaTime);
            other.attachedRigidbody.velocity = other.attachedRigidbody.velocity.normalized * power;
        }
    }
}
