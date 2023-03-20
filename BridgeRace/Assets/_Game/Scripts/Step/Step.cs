using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step : UnityEngine.MonoBehaviour, IColorChanging
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
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(GameTag.CHARACTER))
        {
            if(colorType != other.GetComponent<Character>().colorType)
            {
                if(other.GetComponent<Character>().brickAmount != 0)
                {
                    ChangeColor(other.GetComponent<Character>().colorType);
                    GetComponent<MeshRenderer>().enabled = true;
                    other.GetComponent<Character>().PlaceBrick();
                }
            }
        }
    }
}
