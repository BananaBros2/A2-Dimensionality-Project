using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Windows;
using UnityEngine.SceneManagement;

public class DebugMenuScr : MonoBehaviour
{
    private PauseMenuScript pauseMenuScript;

    private bool debugShowen = false;
    public GameObject debugMenu;

    public TMP_InputField console;

    public bool playerCheated { get; private set; } = false;
    private bool noClipEnabled = false;

    ColorBlock defultButtonColours;

    private void Start()
    {
        debugMenu.SetActive(true);

        Button noClipButton = GameObject.Find("NoClipButton").GetComponent<Button>();
        defultButtonColours = noClipButton.colors;

        GameObject parent = transform.parent.gameObject;
        pauseMenuScript = parent.GetComponentInChildren<PauseMenuScript>();

        debugMenu.SetActive(false);
    }

    private void Update()
    {   
        if (Input.GetButtonDown("Debug Menu")) debugShowen = !debugShowen;

        if (pauseMenuScript.isPaused) return;

        Cursor.lockState = debugShowen ? CursorLockMode.Confined : CursorLockMode.Locked;
        Cursor.visible = debugShowen ? true : false;

        if (Time.timeScale == 0f)
        {
            GameObject parent = transform.parent.gameObject;
            TimerController timerController = parent.GetComponentInChildren<TimerController>();
            timerController.canRestart = false;
        }
        else
        {
            GameObject parent = transform.parent.gameObject;
            TimerController timerController = parent.GetComponentInChildren<TimerController>();
            if (!timerController.canRestart) timerController.canRestart = true;
        }
        debugMenu.SetActive(debugShowen);

        if (playerCheated)
        {
            GameObject parent = transform.parent.gameObject;
            TimerController timerController = parent.GetComponentInChildren<TimerController>();
            timerController.hasCheated = true;
        }
    }

    public void Submit()
    {
        string inputedText = console.GetComponent<TMP_InputField>().text;

        if (inputedText == "NoClip")
        {
            NoClip();
        }
        else
        {
            Debug.Log("Error! unkown command");
        }
    }

    public void NoClip()
    {
        playerCheated = true;

        noClipEnabled = !noClipEnabled;

        Button noClipButton = GameObject.Find("NoClipButton").GetComponent<Button>();
        var buttonColors = noClipButton.colors;
        if (noClipEnabled)
        {
            buttonColors.normalColor = Color.red;
            buttonColors.selectedColor = Color.black;
            noClipButton.colors = buttonColors;
        }
        else
        {
            buttonColors.normalColor = defultButtonColours.normalColor;
            buttonColors.selectedColor = defultButtonColours.selectedColor;
            noClipButton.colors = buttonColors;
        }

        GameObject parent = transform.parent.gameObject;
        BasicPlayerMovementController bpmc = parent.GetComponentInChildren<BasicPlayerMovementController>();
        bpmc.noClip = noClipEnabled;
    }
}
