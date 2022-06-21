using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> AllCheckpointParents = new List<GameObject>();
    private Scene scene;

    private Vector3 currentCheckpointResetPoint;
    public int currentCheckpoint = -1;
    private int TrueCheckpointCount = 0;
    private bool isThereCheckpoints = false;
    public TimerController timerController;

    private AsyncOperation LoadSceneAsync;

    void Start()
    {
        Transform root = transform.root;

        timerController = root.GetComponentInChildren<TimerController>();

        currentCheckpoint = -1;

        scene = SceneManager.GetActiveScene();

        AllCheckpointParents.Clear();

        foreach (GameObject PossibleCheckpoint in scene.GetRootGameObjects())
        {
            if (PossibleCheckpoint.transform.tag == "Checkpoint")
            {
                AllCheckpointParents.Add(PossibleCheckpoint);
            }
        }

        for (int i = 0; i >= AllCheckpointParents.Count; i++)
        {
            print(AllCheckpointParents[i].transform.name);
        }

        PopulateList();

        AllCheckpointParents.Sort(SortByName);

        if (AllCheckpointParents.Count >= 0) isThereCheckpoints = true;

        TrueCheckpointCount = AllCheckpointParents.Count - 1;

    }

    private static int SortByName(GameObject o1, GameObject o2) // simple string sorting by comparing
    {
        return o1.name.CompareTo(o2.name);
    }

    void Update()
    {
        PopulateList();

        if (currentCheckpoint > TrueCheckpointCount) currentCheckpoint = TrueCheckpointCount; // could be simplified

        if (currentCheckpoint >= 0) currentCheckpointResetPoint = AllCheckpointParents[currentCheckpoint].transform.Find("Respawn Point").position;

        // here lies the remains of the old press r to restart and the new hold shift and r to restart

        //if (Input.GetKey(KeyCode.R) && isThereCheckpoints)
        //{
        //    if (!timerController.canRestart) return;

        //    if (Input.GetKey(KeyCode.LeftShift)) LoadSceneAsync = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
            
        //    if (currentCheckpoint == -1) LoadSceneAsync = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        //    else transform.position = currentCheckpointResetPoint;
        //}
    }

    // called so another script can reset the player like the death zones
    public void RestartToChecpoint()
    {
        // this check if the script can restart
        if (!timerController.canRestart) return;

        if (Input.GetKey(KeyCode.LeftShift)) LoadSceneAsync = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);

        if (currentCheckpoint == -1) LoadSceneAsync = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        else transform.position = currentCheckpointResetPoint;
    }

    void PopulateList()
    {
        if (LoadSceneAsync == null) return;

        if (LoadSceneAsync.isDone && AllCheckpointParents.Count == 0)
        {
            AllCheckpointParents.Clear();

            currentCheckpoint = -1;

            foreach (GameObject PossibleCheckpoint in scene.GetRootGameObjects())
            {
                if (PossibleCheckpoint.transform.tag == "Checkpoint")
                {
                    AllCheckpointParents.Add(PossibleCheckpoint);
                }
            }

            print(AllCheckpointParents.Count);

            for (int i = 0; i >= AllCheckpointParents.Count; i++)
            {
                print(AllCheckpointParents[i].transform.name);
            }
        }
    }
}
