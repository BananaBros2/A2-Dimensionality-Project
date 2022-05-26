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
        
        Button noClipButton = GameObject.Find("NoClipButton").GetComponent<Button>();
        var buttonColors = noClipButton.colors;
        buttonColors.normalColor = Color.red;
        noClipButton.colors = buttonColors;

        BasicPlayerMovementController bpmc = GetComponentInParent<BasicPlayerMovementController>();
        print("bpmc");
    }
}
