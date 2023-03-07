using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour, IColorChanging
{
    [SerializeField] private MeshRenderer brickRenderer;

    public ColorType colorType;

    public Data brickData;

    void Start()
    {
        SetRandomColor();
        ChangeColor(colorType);
    }


    public void SetRandomColor()
    {
        colorType = (ColorType)Random.Range(0, 3);
    }



    public void ChangeColor(ColorType color)
    {
        colorType = color;
        brickRenderer.material = brickData.GetMats(color);
    }




}
