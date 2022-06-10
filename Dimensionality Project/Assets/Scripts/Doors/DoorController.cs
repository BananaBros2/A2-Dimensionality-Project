using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject doorRight;
    public GameObject doorLeft;

    public Transform targetRight;
    public Transform targetLeft;

    private Vector3 closedRight;
    private Vector3 closedLeft;

    public float speed;

    private DoorState doorState = DoorState.Closed;

    private bool toOpen = false;
    private bool toClose = false;

    private void Start()
    {
        closedRight = doorRight.transform.position; //gets the position of the door when it is closed
        closedLeft = doorLeft.transform.position; //gets the position of the door when it is closed.

    }

    private void Update()
    {
        if (toOpen)
        {
            OpenDoor(); //if toOpen bool value changes to true, calls OpenDoor function.
        }
        else if (toClose)
        {
            CloseDoor(); //if toClose bool value changes to true, calls CloseDoor function.
        }
    }

    private void OnTriggerEnter(Collider other) // calls if an object enters the collider.
    {
        if (other.tag == "Player") //checks if the thing that collided is the player.
        {
            toClose = false; //changes toClose bool value to false
            toOpen = true; //changes toOpen bool value to true
        }
    }

    private void OnTriggerExit(Collider other) //calls if an object exits the door collider.
    {
        if (other.tag == "Player") //checks if the object exiting the collider is the player.
        {
            toClose = true; //changes toClose bool value to true
            toOpen = false; //changes toOpen bool value to false
        }
    }

    private void OpenDoor() //OpenDoor function
    {
        doorRight.transform.position = Vector3.MoveTowards(doorRight.transform.position, targetRight.position, speed * Time.deltaTime);
        //Moves the Right door towards the target position using a set speed relative to the FPS
        doorLeft.transform.position = Vector3.MoveTowards(doorLeft.transform.position, targetLeft.position, speed * Time.deltaTime);
        //Moves the Left door towards the target position using a set speed relative to the FPS
        if (doorRight.transform.position == targetRight.position)

        {
            doorState = DoorState.Open;
        }
        else
        {
            doorState = DoorState.Opening;
        }//These lines of code made it easy for us to see where the script got up to when using debug.log
    }

    private void CloseDoor() //CloseDoor function
    {
        doorRight.transform.position = Vector3.MoveTowards(doorRight.transform.position, closedRight, speed * Time.deltaTime);
        //Moves the Right door towards the close position using a set speed relative to the FPS
        doorLeft.transform.position = Vector3.MoveTowards(doorLeft.transform.position, closedLeft, speed * Time.deltaTime);
        //Moves the Left door towards the close position using a set speed relative to the FPS
        if (doorRight.transform.position == closedRight)
        {
            doorState = DoorState.Closed;
        }
        else
        {
            doorState = DoorState.Closing;
        }//These lines of code made it easy for us to see where the script got up to when using debug.log
    }

    private enum DoorState
    {
        Closed,
        Opening,
        Open,
        Closing
    }//These lines of code made it easy for us to see where the script got up to when using debug.log

}
