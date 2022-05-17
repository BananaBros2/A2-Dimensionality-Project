using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpGroundedTriggerController : MonoBehaviour
{
    public ExpPlayerStateController stateController;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            stateController.isGrounded = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            stateController.isGrounded = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            stateController.isGrounded = false;
        }
    }
}
