using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IColorChanging
{
    // Start is called before the first frame update
    public  Data charData;

    public ColorType colorType;

    public SkinnedMeshRenderer charRenderer;
    public virtual void ChangeColor(ColorType color)
    {
        colorType = color;
        charRenderer.material = charData.GetMats(color);
    }

    public virtual void SetRandomColor()
    {
        colorType = (ColorType)Random.Range(0, 3);
    }
    public virtual void OnInit()
    {

    }
}
