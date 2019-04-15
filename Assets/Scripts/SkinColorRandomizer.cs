using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SkinColorRandomizer : MonoBehaviour
{
    //https://www.collectedwebs.com/art/colors/skin_tones/

    Renderer renderer;
    public float r, g, b;
    public float a, aMin, aMax;

    public Vector3[] skinTones;
    public Vector3[] redTones;
    public Vector3[] offWhiteTones;

    void Start()
    {
        renderer = GetComponent<Renderer>();
        int i = Random.Range(0, skinTones.Length);
        r = skinTones[i].x / 255f;
        g = skinTones[i].y / 255f;
        b = skinTones[i].z / 255f;
        a = Random.Range(aMin, aMax);
        Color newColor = new Color(r, g, b, a);
        renderer.material.SetColor("_Color", newColor);
    }
}
