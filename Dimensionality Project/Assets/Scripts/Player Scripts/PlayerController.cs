using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float PlayerHeight { get; private set; } = 2f;

    private bool isPaused = false;
    private float updateTick = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UpdatePlayerStats());
    }

    // Update is called once per frame
    void Update()
    { 

    }

    private IEnumerator UpdatePlayerStats()
    {
        while (!isPaused)
        {
            PlayerHeight = GetComponentInChildren<CapsuleCollider>().height * transform.localScale.y;
            yield return new WaitForSeconds(updateTick);
        }
    }
}
