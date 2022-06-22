using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPadScript : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] private Animator Boing_Animator;
    public float power = 200f;

    private float coolDown = 0.1f;
    private float cooldowntime;
    private float time = 0.1f;

    private void Update() { time += Time.deltaTime; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player" && time >= cooldowntime)
        {
            Boing_Animator = GetComponent<Animator>();
            rb = other.GetComponentInParent<Rigidbody>();
            print("check");
            rb.AddForce(transform.up * power, ForceMode.Impulse);
            Boing_Animator.SetTrigger("Boing");
            cooldowntime = time + coolDown;
        }
    }
}
