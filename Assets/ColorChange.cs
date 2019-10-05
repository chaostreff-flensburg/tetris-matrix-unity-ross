using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    public Color color;
    public BrushInfo brushInfo;

    private void OnMouseDown()
    {
        brushInfo.color = color;
    }

    private void Start()
    {
        GetComponent<Renderer>().material.color = color;
    }
}
