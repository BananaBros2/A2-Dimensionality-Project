using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportController : MonoBehaviour
{
    //private float cooldown = 10f;
    CapsuleCollider capsuleCollider;

    public float range = 5f;
    public float coolDown = 1f;

    private float cooldowntime;
    private float time;

    public Vector3 targetPosition;
    Vector3 topOfCap;
    Vector3 botOfCap;

    public GameObject player;

    public Transform or;

    public LayerMask GroundMask;
    
    private float offsetZ;
    private float offsetX;


    private void Start()
    {
        capsuleCollider = GetComponentInChildren<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (Input.GetButtonDown("Teleport"))
        {
            targetPosition = transform.position + or.forward * range;

            if (time >= cooldowntime)
            {
                topOfCap = new Vector3(player.transform.position.x, player.transform.position.y + capsuleCollider.height / 2f - capsuleCollider.radius, player.transform.position.z);
                botOfCap = new Vector3(player.transform.position.x, player.transform.position.y - capsuleCollider.height / 2f + capsuleCollider.radius, player.transform.position.z);
                Teleport(); //calls function
            }
            else
            {
                print("on cool down");
            }
        }

        
    }

    /*
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(topOfCap, capsuleCollider.radius);
        Gizmos.DrawSphere(botOfCap, capsuleCollider.radius);
    }
    */

    void Teleport()
    {
        RaycastHit hit;

        cooldowntime = time + coolDown;
        
        if (Physics.CapsuleCast(topOfCap, botOfCap, capsuleCollider.radius / 1000f, or.forward / 1f, out hit, range))
        {
            print(hit.distance);
            offsetZ = ((player.transform.position.z - hit.point.z) / 2.5f);
            offsetX = ((player.transform.position.x - hit.point.x) / 2.5f);
            player.transform.position = new Vector3(hit.point.x + offsetX, player.transform.position.y, hit.point.z + offsetZ);
        }
        else
        {
            player.transform.position = targetPosition;
            print("aight");
        }
        
    }
}
