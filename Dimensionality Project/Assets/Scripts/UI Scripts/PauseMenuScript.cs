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

    public bool isPaused { get; private set; } = false;

    public Scene MasterScene;
    GameManager GM;

    private void Start()
    {
        int countLoaded = SceneManager.sceneCount;
        Scene[] loadedScenes = new Scene[countLoaded];

        for (int i = 0; i < countLoaded; i++)
        {
            loadedScenes[i] = SceneManager.GetSceneAt(i);
        }

        foreach (Scene x in loadedScenes)
        {
            print(x.name);
            if (x.name == "Master Scene") MasterScene = x;
        }

        foreach (GameObject x in MasterScene.GetRootGameObjects())
        {
            if (x.transform.name == "Game manager") GM = x.GetComponent<GameManager>();
        }

        // save system load

        Time.timeScale = 1f;

        pauseMenu.SetActive(isPaused);

        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    public void TogglePauseMenu()
    {
        isPaused = !isPaused;

        Cursor.lockState = isPaused ? CursorLockMode.Confined : CursorLockMode.Locked;
        Cursor.visible = isPaused ? true : false;
        pauseMenu.SetActive(isPaused);
        if (isPaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }



    public void ReturnToMainMenu()
    {
        pauseMenu.SetActive(false);
        if (GM == null)
        {
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }
        else
            GM.Loadmenu();
    }
}
