using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotAction : MonoBehaviour
{
    public static BotAction ins;
    public int currentFloor;
    public bool IsDestination => Vector3.Distance(new Vector3(transform.position.x, targetedBrick.transform.position.y, transform.position.z), targetedBrick.transform.position) < 0.1f;

    public NavMeshAgent agent;
    public GameObject finalTarget;
    public GameObject targetedBrick;

    //[SerializeField] private GameObject finalTarget;
    [SerializeField] private GameObject[] destination;
    private void Awake()
    {
        ins = this;
    }
    private void Start()
    {
        targetedBrick = null;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //finalTarget = destination[currentFloor];
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
    public void StopMoving()
    {
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
        List<GameObject> brickList = BrickSpawner.ins.floorBricks[BotAction.ins.currentFloor];
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(GameTag.NEXT_FLOOR))
        {
            currentFloor++;
            Destroy(other.gameObject);
        }   
        if(other.CompareTag(GameTag.WIN_ZONE))
        {
            Bot.ins.ChangeAnim(AnimName.CHEER);
            UIManager.Ins.OpenUI<Lose>();
        }
    }
}
