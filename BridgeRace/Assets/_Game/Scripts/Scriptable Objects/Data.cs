using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColorType { Red = 0, Blue = 1, Green = 2, No_Color = 3 }

[CreateAssetMenu(fileName = "New Data", menuName = "Data")]


public class Data : ScriptableObject
{
    [SerializeField] private List<Material> materials;
    public Material GetMats(ColorType color)
    {
        return materials[(int)color];
    }
}
