using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpPlayerStateController : MonoBehaviour
{
    //public float PlayerHeight { get; private set; } = 2f;
    public bool isGrounded = false;

    private bool isPaused = false;
    private float updateTick = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(UpdatePlayerStats());
        //StartCoroutine(OutputPlayerStats());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator UpdatePlayerStats()
    {
        while (!isPaused)
        {
            //PlayerHeight = GetComponentInChildren<CapsuleCollider>().height * transform.localScale.y;
            yield return new WaitForSeconds(updateTick);
        }
    }

    private IEnumerator OutputPlayerStats()
    {
        while (!isPaused)
        {
            //Debug.Log("Player Height: " + PlayerHeight.ToString());
            Debug.Log("Is Grounded?: " + isGrounded);
            yield return new WaitForSeconds(updateTick);
        }
    }
}
