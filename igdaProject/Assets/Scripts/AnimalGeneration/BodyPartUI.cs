using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BodyPartUI : AnimalPartUI
{
    [Header("Body Point Refs")]
    public RectTransform headPoint;
    public RectTransform tailPoint;
    public RectTransform legFR;
    public RectTransform legBR;
    public RectTransform legBL;
    public RectTransform legFL;
}
