using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorRandomizer : MonoBehaviour
{
    Renderer renderer;
    public float r, g, b;

    public float min, max;

    public Vector3[] tones;

    //Off Whites won't need a specific collection of colors
    public bool usingOffWhites;

    void Start()
    {
        renderer = GetComponent<Renderer>();

        if (usingOffWhites)
        {
            r = Random.Range(min, max);
            g = Random.Range(min, max);
            b = Random.Range(min, max);
        }
        else
        {
            //Get a random index for one of the available lists of tones       
            int i = Random.Range(0, tones.Length);

            //Sets the RGB values to ones between 0 and 1
            r = tones[i].x / 255f;
            g = tones[i].y / 255f;
            b = tones[i].z / 255f;
        }

        //For now, alpha will always be 1
        Color newColor = new Color(r, g, b, 1);
        renderer.material.SetColor("_Color", newColor);
    }
}
