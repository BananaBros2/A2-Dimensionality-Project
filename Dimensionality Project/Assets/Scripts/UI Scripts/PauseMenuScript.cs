using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;
using TMPro;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public GameObject pauseMenu;

    private bool isPaused = false;

    private void Start()
    {
        // save system load

        pauseMenu.SetActive(isPaused);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePauseMenu();
        }
    }

    public void TogglePauseMenu()
    {
        isPaused = !isPaused;

        Cursor.lockState = isPaused ? CursorLockMode.Confined : CursorLockMode.Locked;
        Cursor.visible = isPaused;
        pauseMenu.SetActive(isPaused);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
