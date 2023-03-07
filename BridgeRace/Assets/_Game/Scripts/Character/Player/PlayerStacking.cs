using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStacking : MonoBehaviour
{
    private Stack<GameObject> bricks;
    
    
    [SerializeField] private GameObject brickStack;

    public static PlayerStacking ins;

    public int bricksAmount = 0;
    
    private void Awake()
    {
        ins = this;
    }
    private void Start()
    {
        bricks = new Stack<GameObject>();
        bricks.Push(brickStack);
        
    }
    public void AddBrick(GameObject brick)
    {
        Vector3 offset = new Vector3(0, .2f, 0);

        Vector3 lastPosition = bricks.Peek().transform.position;
        Vector3 newPosition = lastPosition + offset;
        
        
        brick.transform.parent = this.transform;
        brick.transform.SetPositionAndRotation(newPosition, bricks.Peek().transform.rotation);
        bricks.Push(brick);
        bricksAmount++;
    }

    
    public void DropBrick()
    {

    }
}
