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
        closedRight = doorRight.transform.position;
        closedLeft = doorLeft.transform.position;
        
    }

    private void Update()
    {
        if (toOpen)
        {
            OpenDoor();
        }
        else if (toClose)
        {
            CloseDoor();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            toClose = false;
            toOpen = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            toClose = true;
            toOpen = false;
        }
    }

    private void OpenDoor()
    {
        doorRight.transform.position = Vector3.MoveTowards(doorRight.transform.position, targetRight.position, speed * Time.deltaTime);
        doorLeft.transform.position = Vector3.MoveTowards(doorLeft.transform.position, targetLeft.position, speed * Time.deltaTime);
        if (doorRight.transform.position == targetRight.position)
        {
            //doorState = DoorState.Open;
        }
        else
        {
            doorState = DoorState.Opening;
        }
    }

    private void CloseDoor()
    {
        doorRight.transform.position = Vector3.MoveTowards(doorRight.transform.position, closedRight, speed * Time.deltaTime);
        doorLeft.transform.position = Vector3.MoveTowards(doorLeft.transform.position, closedLeft, speed * Time.deltaTime);
        if (doorRight.transform.position == closedRight)
        {
            doorState = DoorState.Closed;
        }
        else
        {
            doorState = DoorState.Closing;
        }
    }

    private enum DoorState
    {
        Closed,
        Opening,
        Open,
        Closing
    }

}
