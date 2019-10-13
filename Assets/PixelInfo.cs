using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelInfo : MonoBehaviour
{
    public BrushInfo brushInfo;
    private Renderer _renderer;
    private Color _color;


    private void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    void OnMouseOver()
    {
        _renderer.material.color = brushInfo.color;
        _color = brushInfo.color;
    }


    public Color GetColor()
    {
        return _color;
    }
}