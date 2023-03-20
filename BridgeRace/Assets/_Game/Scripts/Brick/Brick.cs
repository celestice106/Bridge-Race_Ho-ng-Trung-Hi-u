using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour, IColorChanging
{
    [SerializeField] private MeshRenderer brickRenderer;

    public ColorType colorType;

    public Data brickData;

    public void ChangeColor(ColorType color)
    {
        colorType = color;
        brickRenderer.material = brickData.GetMats(color);
    }

    public void SetRandomColor()
    {
        ChangeColor((ColorType)Random.Range(0, 3));
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(GameTag.CHARACTER))
        {
            if (other.GetComponent<Character>().colorType == colorType)
            {
                colorType = ColorType.No_Color;
                gameObject.SetActive(false);
                other.GetComponent<Character>().AddBrick();
            }
        }
    }
}
