using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : Character
{
    public static Bot ins;

    private IState currentState;

    public int currentFloor = 0;

    //private bool isFallen;
    private void Awake()
    {
        ins = this;
    }

    private void Start()
    {
        OnInit();
        SetRandomColor();
    }

    private void Update()
    {
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
    }
    public override void ChangeColor(ColorType color)
    {
        base.ChangeColor(color);
    }

    public override void SetRandomColor()
    {
        base.SetRandomColor();
    }

    public override void OnInit()
    {
        this.brickStack = new Stack<GameObject>();
        this.brickStack.Push(this.bag);
        ChangeState(new FindingState());
        base.OnInit();
    }

    
    public void StopMoving()
    {
        BotAction.ins.agent.velocity = Vector3.zero;
        BotAction.ins.agent.isStopped = true;
        ChangeAnim("Idle");
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            /*case GameTag.BRICK:
                if (this.colorType == other.GetComponent<Brick>().colorType)
                {
                    AddBrick(other.gameObject, botBag, botStack);
                    botBrickAmount++;
                }
                break;*//*
            case GameTag.STEP:
                if (colorType != other.GetComponent<Step>().colorType)
                {
                    if (botBrickAmount != 0)
                    {
                        PlaceBrick(other.gameObject, this.brickStack, this.colorType);
                        botBrickAmount--;
                    }
                }
                break;*/
            case GameTag.NEXT_FLOOR:
                currentFloor++;
                //GetAllBricksPos();
                break;
        }
    }

    
    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = newState;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

}

