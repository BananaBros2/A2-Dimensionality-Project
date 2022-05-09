using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPadScript : MonoBehaviour
{
    Rigidbody rb;

    public float power = 200f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            rb = other.GetComponentInParent<Rigidbody>();
            rb.AddForce(transform.up * power, ForceMode.Impulse);
        }
    }
}
