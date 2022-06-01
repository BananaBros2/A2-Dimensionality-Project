using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{

    public static List<GameObject> AllCheckpointParents = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        GameObject root = transform.root.gameObject;

        foreach (GameObject PossibleCheckpoint in root.transform)
        {
            if (PossibleCheckpoint.transform.tag == "Checkpoint")
            {
                AllCheckpointParents.Add(PossibleCheckpoint);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i > AllCheckpointParents.Count; i++)
        {
            //inset magic
        }
    }
}
