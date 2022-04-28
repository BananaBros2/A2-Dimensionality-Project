using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryTriggerController : MonoBehaviour
{
    public TimerController timerController;

    private void OnTriggerExit(Collider other)
    {
        timerController.ResetTimer();
        timerController.StartTimer();
    }
}
