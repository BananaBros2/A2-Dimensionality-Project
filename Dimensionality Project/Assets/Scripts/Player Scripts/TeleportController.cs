using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportController : MonoBehaviour
{
    CapsuleCollider capsuleCollider;

    public float range = 5f;

    public float coolDown = 2f;
    private float cooldowntime;
    private float time;

    public GameObject player;
    public Transform or;

    public bool IsPlayerNoClipping = false;

    private void Start()
    {
        capsuleCollider = GetComponentInChildren<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (Input.GetButtonDown("Teleport") && time >= cooldowntime && !IsPlayerNoClipping)
        {
            Teleport(); //calls function
        }
    }

    void Teleport()
    {
        RaycastHit hit;
        cooldowntime = time + coolDown;
        Vector3 topOfCap = new Vector3(player.transform.position.x, player.transform.position.y + capsuleCollider.height / 2f - capsuleCollider.radius, player.transform.position.z);
        Vector3 botOfCap = new Vector3(player.transform.position.x, player.transform.position.y - capsuleCollider.height / 2f + capsuleCollider.radius, player.transform.position.z);

        if (Physics.CapsuleCast(topOfCap, botOfCap, capsuleCollider.radius / 1000f, or.forward, out hit, range, ~(1 << 8)))
        {
            float offsetZ = (player.transform.position.z - hit.point.z) / 2.5f;
            float offsetX = (player.transform.position.x - hit.point.x) / 2.5f;
            player.transform.position = new Vector3(hit.point.x + offsetX, player.transform.position.y, hit.point.z + offsetZ);
        }
        else { player.transform.position = transform.position + or.forward * range * capsuleCollider.height / 2f; }
    }
}