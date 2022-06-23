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
    private bool canReload = true;

    public Scene MasterScene;
    GameManager GM;

    // Start is called before the first frame update
    void Start()
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
            if (x.name == "Master Scene") MasterScene = x; GM = GetComponentInChildren<GameManager>();
        }

        foreach (GameObject x in MasterScene.GetRootGameObjects())
        {
            if (x.transform.name == "Game manager") GM = x.GetComponent<GameManager>();
        }

        scene = SceneManager.GetSceneAt(1);
        foreach (GameObject PossibleItem in scene.GetRootGameObjects())
        {
            if (PossibleItem.transform.name == "Importable player asset")
            {
                root = PossibleItem.gameObject;
            }
        }

        CM = root.transform.Find("Player").GetComponent<CheckpointManager>();

        //print(CM.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            if (CM == null)
            {
                if (GM == null)
                {
                    print("NULL REFERNCE | FALIURE");
                    SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single); // safty if statement
                }
                else
                {
                    if (canReload)
                    {
                        print("NULL REFERNCE");
                        GM.Reload();
                        //canReload = false;
                    }
                }
            }
            else
            {
                if (GM == null)
                {
                    print("NULL REFERNCE | FALIURE");
                    SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single); // safty if statement
                }
                else
                {
                    if (canReload)
                    {
                        print("reload!");
                        CM.RestartToChecpoint();
                        //canReload = false;
                    }
                }
            }
        }
    }
}

// done by DM