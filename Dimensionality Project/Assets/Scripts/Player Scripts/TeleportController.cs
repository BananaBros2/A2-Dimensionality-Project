using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeleportController : MonoBehaviour
{
    public Slider coolDownBar;
    public GameObject LPB;
    private ProgressBarManager PBM;

    CapsuleCollider capsuleCollider;
    public PlayerController playerController;

    public float range = 5f;

    public float coolDown = 2f;
    private float cooldowntime;
    private float time;

    public GameObject player;
    public Transform or;

    public bool IsPlayerNoClipping = false;

    private void Start()
    {
        PBM = LPB.GetComponent<ProgressBarManager>();
        PBM.minimum = 0f;
        PBM.maximum = coolDown;
        PBM.current = coolDown;

        capsuleCollider = GetComponentInChildren<CapsuleCollider>();

        //coolDownBar.maxValue = coolDown; // use this if you are using a slider
        //coolDownBar.value = coolDown;
    }

    // Update is called once per frame
    void Update()
    {
        float coolDownCurrentTime = time <= cooldowntime ? coolDown - (cooldowntime - time) : coolDown;
        PBM.current = coolDownCurrentTime;

        //coolDownBar.value = coolDownCurrentTime; // use this if you are using a slider

        LPB.SetActive(coolDownCurrentTime == coolDown ? false : true);

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
        if (Physics.CapsuleCast(topOfCap, botOfCap, 0f, or.forward, out hit, range * playerController.PlayerHeight, ~(1 << 9)))
        {
            float offsetZ = (player.transform.position.z - hit.point.z) / 2.5f;
            float offsetX = (player.transform.position.x - hit.point.x) / 2.5f;
            player.transform.position = new Vector3(hit.point.x + offsetX, player.transform.position.y, hit.point.z + offsetZ);
        }
        else { player.transform.position = transform.position + or.forward * range * playerController.PlayerHeight / 2; }
    }
}