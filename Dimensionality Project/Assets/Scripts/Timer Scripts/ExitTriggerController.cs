using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTriggerController : MonoBehaviour
{
    public TimerController timerController;

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
           timerController.StartCoroutine("StopTimer");
        }
    }
}
