using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotAction : MonoBehaviour
{
    public static BotAction ins;

    public bool IsDestination => Vector3.Distance(new Vector3(transform.position.x, targetedBrick.transform.position.y, transform.position.z), targetedBrick.transform.position) < 0.1f;

    public NavMeshAgent agent;
    public GameObject targetedBrick;

    //[SerializeField] private GameObject finalTarget;
    public GameObject finalTarget;
    private void Awake()
    {
        ins = this;
    }
    private void Start()
    {
        targetedBrick = null;
        agent = GetComponent<NavMeshAgent>();
    }


    public void FindBrick()
    {
        if(targetedBrick == null)
        {
            targetedBrick = FindClosestTarget(Bot.ins.colorType);
        }
        MoveToTarget();
    }

    public void BuildBridge()
    {
        SetDes(finalTarget);
        agent.isStopped = false;
    }

    private void MoveToTarget()
    {
        if (targetedBrick != null)
        {
            SetDes(targetedBrick);
            agent.isStopped = false;
        }
    }


    public void SetDes(GameObject targetedBrick)
    {
        agent.SetDestination(targetedBrick.transform.position);
    }

    public GameObject FindClosestTarget(ColorType color)
    {
        float minDistance = Mathf.Infinity;
        GameObject closestTarget = null;
        List<GameObject> brickList = BrickSpawner.ins.floorBricks[Bot.ins.currentFloor];
        foreach (GameObject brickObject in brickList)
        {
            Brick brick = brickObject.GetComponent<Brick>();
            if(brick.colorType == color)
            {
                float distance = Vector3.Distance(transform.position, brickObject.transform.position);
                if(distance < minDistance)
                {
                    closestTarget = brickObject;
                    minDistance = distance;
                }
            }
        }
        return closestTarget;
    }
}
