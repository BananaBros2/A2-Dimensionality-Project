using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Windows;
using UnityEngine.SceneManagement;

public class DebugMenuScr : MonoBehaviour
{
    private bool debugShowen = false;
    public GameObject debugMenu;

    public TMP_InputField console;

    public bool playerCheated { get; private set; } = false;
    private bool noClipEnabled = false;

    ColorBlock defultButtonColours;

    private void Start()
    {
        Button noClipButton = GameObject.Find("NoClipButton").GetComponent<Button>();
        defultButtonColours = noClipButton.colors;
    }

    private void Update()
    {       
        if (Input.GetButtonDown("Debug Menu")) debugShowen = !debugShowen;

        Cursor.lockState = debugShowen ? CursorLockMode.Confined : CursorLockMode.Locked;
        Cursor.visible = debugShowen ? true : false;
        debugMenu.SetActive(debugShowen);
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
        parent.GetComponentInChildren<BasicPlayerMovementController>();

        BasicPlayerMovementController bpmc = parent.GetComponentInChildren<BasicPlayerMovementController>();
        print(bpmc);
    }
}
