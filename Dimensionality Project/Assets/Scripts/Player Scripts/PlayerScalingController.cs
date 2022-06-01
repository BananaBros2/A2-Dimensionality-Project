using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScalingController : MonoBehaviour
{
    public PlayerController playerController;
    public GameObject scaleUpTrigger;
    public int maxScaleRange;

    public bool IsNoClipEnabled = false;

    public bool IsRoomToScaleUp { get; set; } = true;

    [HideInInspector]
    public int currentScaleIndex = 0;
    private bool scaledThisFrame = false;

    // Update is called once per frame
    void Update()
    {
        if (IsNoClipEnabled) { transform.localScale = new Vector3(2, 2, 2); return; } // sets player back to normal and stops code of running.

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
        if (IsRoomToScaleUp && currentScaleIndex + maxScaleRange < maxScaleRange && !scaledThisFrame)
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
