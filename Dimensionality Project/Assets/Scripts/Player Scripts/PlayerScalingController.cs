using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScalingController : MonoBehaviour
{
    public GameObject[] optionsForScaling = new GameObject[4];

    public PlayerController playerController;
    public GameObject scaleUpTrigger;
    public int maxScaleRange;

    public bool IsNoClipEnabled = false;

    public bool IsRoomToScaleUp { get; set; } = true;

    [HideInInspector]
    public int currentScaleIndex = 0;
    private bool scaledThisFrame = false;

    private void FixedUpdate()
    {
        switch (currentScaleIndex)
        {
            case 0:
                for (int i = 0; i < optionsForScaling.Length; i++)
                {
                    optionsForScaling[i].SetActive(true); 
                }
                optionsForScaling[0].SetActive(false);
                break;

            case -1:
                for (int i = 0; i < optionsForScaling.Length; i++)
                {
                    optionsForScaling[i].SetActive(true);
                }
                optionsForScaling[1].SetActive(false);
                break;

            case -2:
                for (int i = 0; i < optionsForScaling.Length; i++)
                {
                    optionsForScaling[i].SetActive(true);
                }
                optionsForScaling[2].SetActive(false);
                break;

            case -3:
                for (int i = 0; i < optionsForScaling.Length; i++)
                {
                    optionsForScaling[i].SetActive(true);
                }
                optionsForScaling[3].SetActive(false);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (IsNoClipEnabled) { currentScaleIndex = 2; return; } // sets player back to normal and stops code of running.

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
