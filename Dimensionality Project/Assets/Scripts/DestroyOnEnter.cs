using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnEnter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }
}
