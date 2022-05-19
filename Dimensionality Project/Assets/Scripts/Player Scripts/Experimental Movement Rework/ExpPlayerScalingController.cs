using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpPlayerScalingController : MonoBehaviour
{
    public ExpPlayerStateController stateController;
    public GameObject scaleUpTrigger;
    public Rigidbody rb;
    public int maxScaleRange;

    public bool IsRoomToScaleUp { get; set; } = true;

    private int currentScaleIndex = 0;
    private bool scaledThisFrame = false;

    // Update is called once per frame
    void Update()
    {
        scaledThisFrame = false;
        if (Input.GetButtonUp("Scale Down")) scaledThisFrame = ScaleDown();
        if (Input.GetButtonUp("Scale Up")) scaledThisFrame = ScaleUp();
    }

    private bool ScaleDown()
    {
        if (currentScaleIndex > -maxScaleRange && !scaledThisFrame)
        {
            transform.localScale /= 2f;
            currentScaleIndex--;
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool ScaleUp()
    {
        if (IsRoomToScaleUp && currentScaleIndex < maxScaleRange && !scaledThisFrame)
        {
            transform.localScale *= 2f;
            currentScaleIndex++;
            return true;
        }
        else
        {
            return false;
        }
    }
}
