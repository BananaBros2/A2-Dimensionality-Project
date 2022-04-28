using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTriggerController : MonoBehaviour
{
    public TimerController timerController;

    private void OnTriggerExit(Collider other)
    {
        timerController.StartCoroutine("StopTimer");
    }
}
