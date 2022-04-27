using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Windows;

public class Timer : MonoBehaviour
{
    public Text timer;

    float time = 0f;

    public BoxCollider levelEndTrigger;

    public BoxCollider levelStartTrigger;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        print(time);

        time = time + Time.deltaTime;

        int minutes = Mathf.FloorToInt(time / 60f);
        float seconds = time - minutes * 60f;

        seconds.ToString("2F");

        seconds = Mathf.Round(seconds * 100f) / 100f;

        int removeSeconds = Mathf.FloorToInt(time - minutes * 60f);

        float Mili = seconds - removeSeconds;

        string secs;

        string milis;

        if (Mili < 10)
        {
            milis = "0" + Mili.ToString();
        }
        else
        {
            milis = Mili.ToString();
        }

        if (seconds < 10)
        {
            secs = "0" + seconds.ToString();
        }
        else
        {
            secs = seconds.ToString();
        }

        timer.text = minutes.ToString() + ":" + secs + "." + milis;
        
    }
}
