using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPadScript : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] private Animator Boing_Animator;
    public float power = 200f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            Boing_Animator = GetComponent<Animator>();
            rb = other.GetComponentInParent<Rigidbody>();
            rb.AddForce(transform.up * power, ForceMode.Impulse);
            Boing_Animator.SetTrigger("Boing");
        }
    }
}
