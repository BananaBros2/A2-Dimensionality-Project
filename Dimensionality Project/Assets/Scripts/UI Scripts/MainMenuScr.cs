using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuScr : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject levelSelection;

    public Dropdown FullScreenDropdown;
    public TMP_Text LevelVBestTimeText;
    FullScreenMode settingVid;

    private string LevelVbestTime;

    // Start is called before the first frame update
    void Start()
    {
        if (Save_Manager.instance.hasLoaded)
        {
            FullScreenDropdown.value = Save_Manager.instance.saveData.fullscreenMode;

            LevelVbestTime = Save_Manager.instance.saveData.levelVBestTime;
        }
        else
        {
            Save_Manager.instance.saveData.fullscreenMode = 4;

            Save_Manager.instance.saveData.levelVBestTime = "0:00.00";
        }

        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
        levelSelection.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Screen.fullScreenMode = settingVid;

        //other funtions that need to run constantly
        FullScreen();
        Resolution();


        LevelVbestTime = Save_Manager.instance.saveData.levelVBestTime;
        LevelVBestTimeText.text = "Best time: " + LevelVbestTime;
    }

    //sets the fullscreen mode depending on the dropbox value
    public void FullScreen()
    {
        switch (FullScreenDropdown.value)
        {
            case 0:
                settingVid = FullScreenMode.Windowed;
                break;

            case 1:
                settingVid = FullScreenMode.MaximizedWindow;
                break;

            case 2:
                settingVid = FullScreenMode.FullScreenWindow;
                break;

            case 3:
                settingVid = FullScreenMode.ExclusiveFullScreen;
                break;
        }
    }

    //set the resolution for the screen depending of the dropbox value.
    public void Resolution()
    {
        //switch (VideoResolution.value)
        //{
        //    case 0:
        //        Screen.SetResolution(4096, 2160, settingVid, 60);
        //        break;

        //    case 1:
        //        Screen.SetResolution(3840, 2160, settingVid, 60);
        //        break;

        //    case 2:
        //        Screen.SetResolution(2048, 1152, settingVid, 60);
        //        break;

        //    case 3:
        //        Screen.SetResolution(1920, 1080, settingVid, 60);
        //        break;

        //    case 4:
        //        Screen.SetResolution(1280, 720, settingVid, 60);
        //        break;

        //    case 5:
        //        Screen.SetResolution(640, 480, settingVid, 60);
        //        break;
        //}
    }


    //when called all the user's data will be whiped and reset
    public void DeleteAllUserData()
    {
        Save_Manager.instance.DeleteSaveData();

        //Save_Manager.instance.saveData.masterVolumeSave = 1f;

        Save_Manager.instance.saveData.fullscreenMode = 4;

        Save_Manager.instance.saveData.levelVBestTime = "0:00.00";

        Save_Manager.instance.saveData.levelVbestMinutes = 0;

        Save_Manager.instance.saveData.leveLVbestSeconds = 0f;

        Save_Manager.instance.saveData.levelVbestMilliseconds = 0f;

        Save_Manager.instance.saveData.levelVNewTime = true;

        Save_Manager.instance.Save();

        LoadSettings();
    }


    //save values after the button is clicked
    public void SaveSettings()
    {
        //save the values that are set
        //Save_Manager.instance.saveData.masterVolumeSave = masterVolume;

        Save_Manager.instance.saveData.fullscreenMode = FullScreenDropdown.value;

        //Save_Manager.instance.saveData.ScreenResolution = VideoResolution.value;

        //force save
        Save_Manager.instance.Save();
    }

    //loads the settings if the settings have been changed before.
    public void LoadSettings()
    {
        //force load the current values
        Save_Manager.instance.Load();

        //save values
        //masterVolume = Save_Manager.instance.saveData.masterVolumeSave;
        //MasterVolume.value = Save_Manager.instance.saveData.masterVolumeSave;

        FullScreenDropdown.value = Save_Manager.instance.saveData.fullscreenMode;

        //VideoResolution.value = Save_Manager.instance.saveData.ScreenResolution;

        //highScore = Save_Manager.instance.saveData.HighScore;
    }

    // functions for the UI buttons


    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadLevelV()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void LoadTutorialMap()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }

    public void LoadMap003()
    {
        SceneManager.LoadScene(3, LoadSceneMode.Single);
    }

    public void LoadMap004()
    {
        SceneManager.LoadScene(4, LoadSceneMode.Single);
    }
}
