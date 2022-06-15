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
            //RestartLevel(other);
            this.GetComponent<BoxCollider>().enabled = false;
        }
    }

    //private IEnumerable RestartLevel(Collider Other)
    //{
    //    print("yes");
    //    yield return new WaitForSeconds(10f);
    //    callFunction(Other);
    //}

    //private void callFunction(Collider Other)
    //{
    //    CheckpointManager CM = Other.transform.root.transform.GetComponentInChildren<CheckpointManager>();
    //    CM.ResetScene();
    //}
}
