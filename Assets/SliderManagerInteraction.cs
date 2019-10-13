using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SliderManagerInteraction : MonoBehaviour, IPointerExitHandler
{

    public Main main;
    
    public void OnPointerExit(PointerEventData eventData)
    {
        main.SendBrightness();
    }
}
