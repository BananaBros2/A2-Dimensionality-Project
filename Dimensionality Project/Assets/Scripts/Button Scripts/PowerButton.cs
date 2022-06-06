using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerButton : MonoBehaviour
{
    [SerializeField] private Animator Animator;
    public bool Pressed = false;
    public bool Timed = false;

    public float coolDown = 10f;
    private float cooldowntime;
    private float time;


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            Animator = GetComponent<Animator>();
            Animator.SetBool("Pressed", true);
            Pressed = true;
            cooldowntime = time + coolDown;
        }
    }

    private void Update()
    {
        if (Pressed == true && Timed == true)
        {
            time += Time.deltaTime;
            if (time >= cooldowntime)
            {
                Animator.SetBool("Pressed", false);
                Pressed = false;
                cooldowntime = time + coolDown;
            }
        }
    }
}