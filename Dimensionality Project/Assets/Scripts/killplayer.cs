using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class killplayer : MonoBehaviour
{
    // need check or game breaky lel?
    private CheckpointManager CM;
    private Scene scene;
    private GameObject root;

    private void Start()
    {
        scene = SceneManager.GetActiveScene();
        foreach (GameObject PossibleItem in scene.GetRootGameObjects())
        {
            if (PossibleItem.transform.name == "Importable player asset")
            {
                root = PossibleItem.gameObject;
            }
        }

        CM = root.transform.Find("Player").GetComponent<CheckpointManager>();

        print(CM.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            if (CM == null)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single); // safty if statement
                print("NULL REFERNCE");
            }
            else
            {
                CM.RestartToChecpoint();
            }
        }
    }
}

// done by DM