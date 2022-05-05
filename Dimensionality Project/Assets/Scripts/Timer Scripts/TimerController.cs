using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Windows;
using UnityEngine.SceneManagement;

public class TimerController : MonoBehaviour
{
    public Text timerText;

    float time = 0f;

    bool isRunning = false;

    public BoxCollider levelEndTrigger;

    public BoxCollider levelStartTrigger;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning) time += Time.deltaTime;

        int minutes = Mathf.FloorToInt(time / 60f);
        float seconds = time - minutes * 60f;
        float miliseconds = seconds - Mathf.Floor(seconds);

        miliseconds = Mathf.Floor(miliseconds * 100f);
        seconds = Mathf.Floor(seconds);

        timerText.text = minutes.ToString() + ":" + (seconds < 10f ? "0" : "") + seconds.ToString() + "." + (miliseconds < 10f ? "0" : "") + miliseconds.ToString();
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Better use build not editor"); // don't use my computer please but use the builds on teams or github.com
            SceneManager.LoadScene(2, LoadSceneMode.Single);
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
