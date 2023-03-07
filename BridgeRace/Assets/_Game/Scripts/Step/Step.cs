using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step : MonoBehaviour, IColorChanging
{
    public Data stepData;

    public ColorType colorType;

    [SerializeField] private MeshRenderer stepRenderer;

    private void Start()
    {
        colorType = ColorType.No_Color;
        stepRenderer.enabled = false;
    }
    public void ChangeColor(ColorType color)
    {
        this.colorType = color;
        stepRenderer.material = stepData.GetMats(color);
    }

    public void SetRandomColor()
    {
        return;
    }
}
