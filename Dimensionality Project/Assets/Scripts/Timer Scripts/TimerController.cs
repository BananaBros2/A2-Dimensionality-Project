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

    float time = 0f;

    bool isRunning = false;

    private bool isVisible = false;

    public BoxCollider levelEndTrigger;

    public BoxCollider levelStartTrigger;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single); // did some googling.
        }

        if (hasCheated)
        {
            time = 0f;
            StopTimer();

            timerText.text = "CHEATED RESTART THE GAME";
            return;
        }

        if (isRunning) time += Time.deltaTime;

        int minutes = Mathf.FloorToInt(time / 60f);
        float seconds = time - minutes * 60f;
        float miliseconds = seconds - Mathf.Floor(seconds);

        miliseconds = Mathf.Floor(miliseconds * 100f);
        seconds = Mathf.Floor(seconds);

        timerText.text = minutes.ToString() + ":" + (seconds < 10f ? "0" : "") + seconds.ToString() + "." + (miliseconds < 10f ? "0" : "") + miliseconds.ToString();

        timer.SetActive(isVisible);

        if (Input.GetKeyDown(KeyCode.T))
        {
            isVisible = !isVisible;
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
    }

}
