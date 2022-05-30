using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Windows;
using UnityEngine.SceneManagement;

public class TimerController : MonoBehaviour
{
    public bool hasCheated = false;

    public Text timerText;
    public GameObject timer;
    public BoxCollider levelEndTrigger;
    public BoxCollider levelStartTrigger;

    float time = 0f;
    bool isRunning = false;
    public bool canRestart = true;
    private bool isVisible = false;
    private bool beatenBestTime = false;

    private string currentTime;
    private int minutes;
    private float seconds;
    private float miliseconds;

    private string bestTime;
    private int bestMinutes = 0;
    private float bestSeconds = 0f;
    private float bestMilliseconds = 0f;



    private void Start()
    {
        if (Save_Manager.instance.hasLoaded)
        {
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                bestTime = Save_Manager.instance.saveData.levelVBestTime;

                bestMinutes = Save_Manager.instance.saveData.levelVbestMinutes;

                bestSeconds = Save_Manager.instance.saveData.leveLVbestSeconds;

                bestMilliseconds = Save_Manager.instance.saveData.levelVbestMilliseconds;
            }
        }
        else
        {
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                Save_Manager.instance.saveData.levelVBestTime = "0:00.00";

                Save_Manager.instance.saveData.levelVbestMinutes = 0;

                Save_Manager.instance.saveData.leveLVbestSeconds = 0f;

                Save_Manager.instance.saveData.levelVbestMilliseconds = 0f;
            }
        }
    }

    void Update()
    {
        timer.SetActive(isVisible);

        if (Input.GetKeyDown(KeyCode.T))
        {
            isVisible = !isVisible;
        }

        if (Input.GetKeyDown(KeyCode.R) && canRestart)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        }

        if (hasCheated)
        {
            time = 0f;
            StopTimer();

            timerText.text = "CHEATED RESTART THE GAME";
            return;
        }

        if (isRunning) time += Time.deltaTime;

        minutes = Mathf.FloorToInt(time / 60f);
        seconds = time - minutes * 60f;
        miliseconds = seconds - Mathf.Floor(seconds);

        miliseconds = Mathf.Floor(miliseconds * 100f);
        seconds = Mathf.Floor(seconds);

        currentTime = minutes.ToString() + ":" + (seconds < 10f ? "0" : "") + seconds.ToString() + "." + (miliseconds < 10f ? "0" : "") + miliseconds.ToString();
        timerText.text = currentTime;

        if ((minutes <= bestMinutes && seconds <= bestSeconds && miliseconds <= bestMilliseconds) || (bestMilliseconds == 0f && bestMinutes == 0 && bestSeconds == 0f)) // please fix
        {
            timerText.color = Color.green;
            beatenBestTime = true;
            bestTime = currentTime;
            bestMinutes = minutes;
            bestSeconds = seconds;
            bestMilliseconds = miliseconds;
        }
        else
        {
            beatenBestTime = true;
            timerText.color = Color.red;
        }
    }

    public void StartTimer()
    {
        isRunning = true;
    }

    public void ResetTimer()
    {
        time = 0f;
    }

    public IEnumerator StopTimer()
    {
        isRunning = false;
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.1f);
            timerText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.1f);
            timerText.gameObject.SetActive(true);
        }
        if (!hasCheated && beatenBestTime)
        {
            Save_Manager.instance.saveData.levelVBestTime = bestTime;
            Save_Manager.instance.saveData.levelVbestMinutes = bestMinutes;
            Save_Manager.instance.saveData.leveLVbestSeconds = bestSeconds;
            Save_Manager.instance.saveData.levelVbestMilliseconds = bestMilliseconds;
        }
    }

}
