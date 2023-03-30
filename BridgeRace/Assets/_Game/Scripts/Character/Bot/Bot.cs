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

        if(isFalling)
        { 
            BotAction.ins.StopMoving();
        }
        if(!isFalling && BotAction.ins.agent.isStopped == false)
        {
            ChangeAnim(AnimName.RUN);
        }
    }
    public override void ChangeColor(ColorType color)
    {
        base.ChangeColor(color);
    }

    public override void SetRandomColor()
    {
        this.colorRange = base.colorRange - 1;
        base.SetRandomColor();
    }

    protected override void OnInit()
    {
        this.brickStack = new Stack<GameObject>();
        ChangeState(new FindingState());
        base.OnInit();
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

