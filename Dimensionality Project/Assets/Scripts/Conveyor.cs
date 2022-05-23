using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MonoBehaviour
{
    public float power = 2f;
    [HideInInspector]
    public Vector3 conveyorForce;
    [SerializeField] Animation Animation;
    void Start()
    {

        Animation = GetComponent<Animation>();

        /*
        mainAnimator.SetFloat("test", power);
        print(mainAnimator.GetFloat("test"));
        mainAnimator.speed = power;
        */

        Animation["Turn"].speed = 1f;
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            Rigidbody rb = other.GetComponentInParent<Rigidbody>();
            other.GetComponentInParent<BasicPlayerMovementController>().conveyorForce = -transform.right * power / 30;
        }
        else if (other.transform.tag == "RigidBody")
        {
            other.transform.Translate(-transform.right * power * Time.deltaTime *1.75f);
            if (other.attachedRigidbody.velocity.magnitude > power)
            {
                other.attachedRigidbody.velocity = other.attachedRigidbody.velocity.normalized * power;
            }
        }
    }
}
