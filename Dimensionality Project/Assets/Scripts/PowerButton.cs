using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerButton : MonoBehaviour
{
    [SerializeField] private Animator Animator;
    public bool Pressed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            Animator = GetComponent<Animator>();
            Animator.SetTrigger("Pressed");
            Pressed = true;
        }
    }
}