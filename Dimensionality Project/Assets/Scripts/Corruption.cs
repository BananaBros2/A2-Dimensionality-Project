using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corruption : MonoBehaviour
{
    Color originalColour;
    Vector2 originalTexOffset;
    Vector2 originalTexScale;
    Vector3 originalObjectScale;

    public bool colourChange = true;
    public bool offsetChange = true;
    public bool textureScaleChange = true;
    public bool objectScale = false;

    public int speed = 10;

    private void Start()
    {
        originalColour = GetComponent<Renderer>().material.GetColor("_Color");
        originalTexOffset = GetComponentInChildren<Renderer>().material.GetTextureOffset("_MainTex");
        originalTexScale = GetComponentInChildren<Renderer>().material.GetTextureScale("_MainTex");
        originalObjectScale = GetComponent<Transform>().localScale;
    }

    private float cooldowntime;
    private float time;

    private void Update()
    {
        time += Time.deltaTime;

        if (time >= cooldowntime)
        {
            if (Random.Range(1, 100) == 1 && time >= cooldowntime && colourChange == true)
            {
                GetComponentInChildren<Renderer>().material.SetColor("_Color", new Color(Random.Range(1f, 5f), Random.Range(1f, 5f), Random.Range(1f, 5f), Random.Range(1f, 5f)));
                cooldowntime = time + (Random.Range(1f, 20f) / speed);
            }

            if (Random.Range(1, 100) == 1 && time >= cooldowntime && offsetChange == true)
            {
                GetComponentInChildren<Renderer>().material.mainTextureOffset = new Vector2(Random.Range(0f, 16f), Random.Range(0f, 16f));
                cooldowntime = time + (Random.Range(1f, 20f) / speed);
            }

            if (Random.Range(1, 100) == 1 && time >= cooldowntime && textureScaleChange == true)
            {
                GetComponentInChildren<Renderer>().material.mainTextureScale = new Vector2(Random.Range(0.1f, 5f), Random.Range(0.1f, 5f));
                cooldowntime = time + (Random.Range(1f, 20f) / speed);
            }

            if (Random.Range(1, 100) == 1 && time >= cooldowntime && objectScale == true)
            {
                GetComponent<Transform>().localScale = new Vector3(Random.Range(1f, 1.1f), Random.Range(1f, 1.1f), Random.Range(1f, 1.1f));
                cooldowntime = time + (Random.Range(1f, 20f) / speed);
            }
        }

        if (Random.Range(1, 100) <= 10 && time * 3 >= cooldowntime)
        {
            GetComponentInChildren<Renderer>().material.SetColor("_Color", originalColour);
            GetComponentInChildren<Renderer>().material.mainTextureOffset = originalTexOffset;
            GetComponentInChildren<Renderer>().material.mainTextureScale = originalTexScale;
            GetComponent<Transform>().localScale = originalObjectScale;
        }
    }

}
