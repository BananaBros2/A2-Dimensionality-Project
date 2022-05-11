using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleUpTriggerController : MonoBehaviour
{
    public PlayerScalingController scalingController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            scalingController.IsRoomToScaleUp = false;
            Debug.Log(scalingController.IsRoomToScaleUp.ToString());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            scalingController.IsRoomToScaleUp = true;
            Debug.Log(scalingController.IsRoomToScaleUp.ToString());
        }
    }
}
