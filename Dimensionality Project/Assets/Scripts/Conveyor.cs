using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MonoBehaviour
{
    private float power = 2.5f;

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            Rigidbody rb = other.GetComponentInParent<Rigidbody>();
            rb.AddForce(-transform.right * power, ForceMode.Impulse);
        }
        else if (other.transform.tag == "RigidBody")
        {
            other.transform.Translate(-transform.right * power * Time.deltaTime);
            if (other.attachedRigidbody.velocity.magnitude > power)
            {
                other.attachedRigidbody.velocity = other.attachedRigidbody.velocity.normalized * power;
            }
        }
    }
}
