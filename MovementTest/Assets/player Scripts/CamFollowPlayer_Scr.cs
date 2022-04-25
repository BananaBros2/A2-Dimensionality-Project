using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowPlayer_Scr : MonoBehaviour
{
    [SerializeField] Transform cameraPosition; // gets the camera position game object

    void Update()
    {
        transform.position = cameraPosition.position; // sets the camera position to the camera position on the player hirachy in the inspector
    }
}
