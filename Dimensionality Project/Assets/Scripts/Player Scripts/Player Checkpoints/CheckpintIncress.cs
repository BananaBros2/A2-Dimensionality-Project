using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpintIncress : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        CheckpointManager CM = other.GetComponentInParent<CheckpointManager>();

        if (other.transform.tag == "Player")
        {
            CM.currentCheckpoint++;
            this.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
