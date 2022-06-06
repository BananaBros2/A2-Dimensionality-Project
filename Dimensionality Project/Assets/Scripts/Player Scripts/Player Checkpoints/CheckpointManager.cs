using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointManager : MonoBehaviour
{
    public static List<GameObject> AllCheckpointParents = new List<GameObject>();
    private Scene scene;

    private Vector3 currentCheckpontRestPoint;
    public int currentCheckpoint = -1;
    private int TrueCheckpointCount = 0;
    private bool isThereCheckpoints = false;
    public TimerController timerController;

    void Start()
    {
        currentCheckpoint = -1;

        Transform root = transform.root;
        scene = SceneManager.GetActiveScene();

        foreach (GameObject PossibleCheckpoint in scene.GetRootGameObjects())
        {
            if (PossibleCheckpoint.transform.tag == "Checkpoint")
            {
                AllCheckpointParents.Add(PossibleCheckpoint);
            }
        }

        AllCheckpointParents.Sort(SortByName);

        if (AllCheckpointParents.Count > 0) isThereCheckpoints = true;

        TrueCheckpointCount = AllCheckpointParents.Count - 1;

        timerController = root.GetComponentInChildren<TimerController>();
    }

    private static int SortByName(GameObject o1, GameObject o2) // simple string sorting by comparing
    {
        return o1.name.CompareTo(o2.name);
    }

    void Update()
    {
        if (currentCheckpoint > TrueCheckpointCount) currentCheckpoint = TrueCheckpointCount; // could be simplified

        if (currentCheckpoint > -1) currentCheckpontRestPoint = AllCheckpointParents[currentCheckpoint].transform.Find("Respawn Point").position;

        if (Input.GetKey(KeyCode.R) && isThereCheckpoints)
        {
            if (!timerController.canRestart) return;

            if (currentCheckpoint == -1) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
            else transform.position = currentCheckpontRestPoint;
        }
    }
}
