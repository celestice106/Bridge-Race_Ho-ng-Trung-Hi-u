using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public static Player ins;

    private void Awake()
    {
        ins = this;
    }
    private void Start()
    {
        OnInit();
        SetRandomColor();
        ChangeColor(this.colorType);
    }

    

    public override void OnInit()
    {
        base.OnInit();
    }

    public override void SetRandomColor()
    {
        base.SetRandomColor();
    }
    public override void ChangeColor(ColorType color)
    {
        base.ChangeColor(color);
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case GameTag.BRICK:
                if (this.colorType == other.GetComponent<Brick>().colorType)
                {
                    BrickSpawing.ins.GetTakenBricksPos(other.gameObject);
                    StartCoroutine(BrickSpawing.ins.SpawnBrick());
                    PlayerStacking.ins.AddBrick(other.gameObject);
                }

                break;
            case GameTag.STEP:
                if( other.GetComponent<Step>().colorType != this.colorType)
                {
                    if(PlayerStacking.ins.bricksAmount != 0)
                    {
                        other.GetComponent<Step>().ChangeColor(this.colorType);
                        other.GetComponent<MeshRenderer>().enabled = true;
                        PlayerStacking.ins.bricksAmount--;
                    }
                }
                break;
        }

    }


    
}
