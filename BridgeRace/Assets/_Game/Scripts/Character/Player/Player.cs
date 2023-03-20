using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public static Player ins;

    //[SerializeField] private CharStacking charStacking;

    private void Awake()
    {
        ins = this;
    }
    private void Start()
    {
        OnInit();
        SetRandomColor();
    }


    public override void OnInit()
    {
        this.brickStack = new Stack<GameObject>();
        this.brickStack.Push(this.bag);
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
        /*switch (other.tag)
        {
            *//*case GameTag.BRICK:
                if (this.colorType == other.GetComponent<Brick>().colorType)
                {
                    AddBrick(other.gameObject, playerBag, playerStack);
                    playerBrickAmount++;
                    Debug.Log("playerBrickAmount: " + playerBrickAmount);
                }
                break;*//*
            case GameTag.STEP:
                if (colorType != other.GetComponent<Step>().colorType)
                {
                    if (playerBrickAmount != 0)
                    {
                        PlaceBrick(other.gameObject, this.brickStack, this.colorType);
                        playerBrickAmount--;
                    }
                }
                break;
        }*/
    }



}
