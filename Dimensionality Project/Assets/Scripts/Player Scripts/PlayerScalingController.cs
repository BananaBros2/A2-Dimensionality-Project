using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScalingController : MonoBehaviour
{
    public PlayerController playerController;
    public int maxScaleRange;

    private int currentScaleIndex = 0;
    private bool scaledThisFrame = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scaledThisFrame = false;
        if (Input.GetButtonUp("Scale Down")) scaledThisFrame = ScaleDown();
        if (Input.GetButtonUp("Scale Up")) scaledThisFrame = ScaleUp();
    }

    private bool ScaleDown()
    {
        if (currentScaleIndex > maxScaleRange * -1 && !scaledThisFrame)
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
        if (isRoomToScaleUp() && currentScaleIndex < maxScaleRange && !scaledThisFrame)
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

    private bool isRoomToScaleUp()
    {
        // TODO: implement
        return true;
    }
}
