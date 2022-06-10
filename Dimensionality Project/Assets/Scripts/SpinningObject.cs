using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningObject : MonoBehaviour
{
    public float Speed = 1;
    public Transform orientation;
    private float Xvalue;
    private float Yvalue;
    private float Zvalue;

    // Update is called once per frame

    private void Start()
    {
        Xvalue = orientation.transform.rotation.x;
        Yvalue = orientation.transform.rotation.y;
        Zvalue = orientation.transform.rotation.z;
    }
    void Update()
    {
        Yvalue += Speed * Time.deltaTime;
        orientation.transform.rotation = Quaternion.Euler(Xvalue, Yvalue, Zvalue);
    }
}
