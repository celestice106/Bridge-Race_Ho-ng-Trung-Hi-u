using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    public int currentFloor = 0;
    public bool IsDestination => Vector3.Distance(new Vector3(transform.position.x, targetedBrick.transform.position.y, transform.position.z), targetedBrick.transform.position) < 0.1f;
    public NavMeshAgent agent;
    public GameObject targetedBrick;
    public GameObject finalTarget;
    private IState currentState;
    //private bool isFallen;
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

        if (isFalling)
        {
            StopMoving();
        }
        if (!isFalling && agent.isStopped == false)
        {
            ChangeAnim(AnimName.RUN);
        }
        if(LevelManager.Ins.isFinished)
        {
            StopMoving();
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
    public void FindBrick()
    {
        if (targetedBrick == null)
        {
            targetedBrick = FindClosestTarget(colorType);
        }
        MoveToTarget();
    }
    public void BuildBridge()
    {
        SetDes(finalTarget);
        agent.isStopped = false;
    }
    public void StopMoving()
    {
        ChangeAnim(AnimName.IDLE);
        agent.velocity = Vector3.zero;
        agent.isStopped = true;
    }
    public void SetDes(GameObject targetedBrick)
    {
        agent.SetDestination(targetedBrick.transform.position);
    }
    public GameObject FindClosestTarget(ColorType color)
    {
        float minDistance = Mathf.Infinity;
        GameObject closestTarget = null;
        List<GameObject> brickList = BrickSpawner.ins.floorBricks[currentFloor];
        foreach (GameObject brickObject in brickList)
        {
            Brick brick = brickObject.GetComponent<Brick>();
            if (brick.colorType == color)
            {
                float distance = Vector3.Distance(transform.position, brickObject.transform.position);
                if (distance < minDistance)
                {
                    closestTarget = brickObject;
                    minDistance = distance;
                }
            }
        }
        return closestTarget;
    }
    protected override void OnInit()
    {
        this.brickStack = new Stack<GameObject>();
        ChangeState(new FindingState());
        targetedBrick = null;
        agent = GetComponent<NavMeshAgent>();
        base.OnInit();
    }
    private void MoveToTarget()
    {
        if (targetedBrick != null)
        {
            SetDes(targetedBrick);
            agent.isStopped = false;
        }
    }

   
}

