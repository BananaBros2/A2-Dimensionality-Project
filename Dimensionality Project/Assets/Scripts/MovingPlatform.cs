using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public bool CanMove = true;
    public bool MovingRight = true;


    public float speed = 3f;

    public Transform LeftTarget;
    public Transform RightTarget;
    public GameObject Platform;

    //Variables for the code.

    void Update() //update function calls once per frame.
    {
        if (CanMove) //if the CanMove bool is true, it will go through the if statement.
        {

            if (MovingRight) //if MovingRight bool is true, it will go through the if statement. 
            {
                Platform.transform.position = Vector3.MoveTowards(Platform.transform.position, RightTarget.position, speed * Time.deltaTime);
                // Moves platform to the right relative to the players FPS 
            }
            else
            {
                Platform.transform.position = Vector3.MoveTowards(Platform.transform.position, LeftTarget.position, speed * Time.deltaTime);
                // Moves platform to the left relative to the players FPS 
            }

            if (Platform.transform.position.Equals(RightTarget.position)) // if the position of the platform reaches the position of the target. 
            {
                MovingRight = false; //sets MovingRight bool to false
            }

            if (Platform.transform.position.Equals(LeftTarget.position)) // if the position of the platform reaches the position of the target. 
            {
                MovingRight = true; //sets MovingRight bool to false 
            }

        }
    }

}

