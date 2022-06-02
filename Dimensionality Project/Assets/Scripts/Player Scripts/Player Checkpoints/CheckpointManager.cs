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
    }

    private static int SortByName(GameObject o1, GameObject o2)
    {
        return o1.name.CompareTo(o2.name);
    }

    void Update()
    {
        if (currentCheckpoint > TrueCheckpointCount) currentCheckpoint = TrueCheckpointCount;

        if (currentCheckpoint > -1) currentCheckpontRestPoint = AllCheckpointParents[currentCheckpoint].transform.Find("Respawn Point").position;

        if (Input.GetKey(KeyCode.K) && isThereCheckpoints)
        {
            if (currentCheckpoint == -1) print(""); // for testing purposes
            else // rip the reset from timer script
            {
                transform.position = currentCheckpontRestPoint;
            }
        }
    }
}
