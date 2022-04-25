using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;
using TMPro;
using UnityEditor.SceneManagement;

public class MainMenuScr : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadMap001()
    {
        EditorSceneManager.LoadScene(1, UnityEngine.SceneManagement.LoadSceneMode.Single);
    }

    public void OutErrorNotFound()
    {
        Debug.LogError("Unable to load. Reason: Missing component");
    }
}
