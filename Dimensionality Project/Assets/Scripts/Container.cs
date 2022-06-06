using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    [SerializeField] public List<GameObject> buttons;

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i <= buttons.Count - 1; i++)
        {
            //print(buttons[i]);
            if (buttons[i].GetComponent<PowerButton>().Pressed == false)
            {
                print(i);
                return;
            }
            if (i == buttons.Count - 1)
            {
                print("Cheese");
                Destroy(this.gameObject);
            }
        }
    }
}
