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

    public PlayerController playerController;

    public Vector3 targetPosition;
    Vector3 topOfCap;
    Vector3 botOfCap;

    public GameObject target;
    public GameObject player;

    public Transform or;

    public LayerMask GroundMask;

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

            //targetPosition = target.transform.position; //sets the position that the player will teleport to

            targetPosition = transform.position + or.forward * range;

            topOfCap = new Vector3(player.transform.position.x, player.transform.position.y + capsuleCollider.height / 2f, player.transform.position.z);
            botOfCap = new Vector3(player.transform.position.x, player.transform.position.y - capsuleCollider.height / 2f, player.transform.position.z);

            if (time >= cooldowntime)
            {
                Teleport(); //calls function

            }
            else
            {
                print("on cool down");
            }
        }

        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(topOfCap, 0.5f);
        Gizmos.DrawSphere(botOfCap, 0.5f);
    }

    void Teleport()
    {
        RaycastHit hit;

        cooldowntime = time + coolDown;
        
        if (Physics.CapsuleCast(topOfCap, botOfCap, capsuleCollider.radius * playerController.PlayerHeight / 2f, or.forward, out hit, range))
        {
            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, hit.point.z);
        }
        else
        {
            player.transform.position = targetPosition;
        }
        
    }
}
