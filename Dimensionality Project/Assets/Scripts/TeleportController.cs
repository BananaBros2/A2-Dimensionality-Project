using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportController : MonoBehaviour
{
    //private float cooldown = 10f;

    public float range = 5f;

    public Vector3 targetPosition;

    public GameObject target;
    public GameObject player;

    public Transform or;

    public LayerMask GroundMask;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Teleport"))
        {

            //targetPosition = target.transform.position; //sets the position that the player will teleport to

            targetPosition = transform.position + or.forward * range;

            Teleport(); //calls function
        }

        
    }

    void Teleport()
    {
        RaycastHit hit;
        Rigidbody rb = GetComponent<Rigidbody>();
        CapsuleCollider capsuleCollider = GetComponentInChildren<CapsuleCollider>();
        //Vector3 topPoint = Vector3.up * transform.position.y / 2f;
        //Vector3 bottomPoint = transform.position - Vector3.down * 2;

        //// Cast character controller shape 10 meters forward to see if it is about to hit anything.
        //if (Physics.CapsuleCast(topPoint, bottomPoint, capsuleCollider.radius,  transform.forward, out hit, 10))
        //player.transform.position = hit.transform.position; // hit.distance;

        if (Physics.CapsuleCast(transform.position, targetPosition, rb.transform.localScale.x, or.forward, out hit, 10000))
        {
            print(hit.transform.name);
        }
        else
        {
            player.transform.position = targetPosition;
        }
    }
}
