using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IColorChanging
{
    public bool isFalling;
    public int brickAmount;

    public Data charData;

    public ColorType colorType;

    protected string currentAnimName;

    protected int colorRange = 3;

    protected Stack<GameObject> brickStack;

    [SerializeField] protected GameObject bag;

    [SerializeField] private Animator anim;

    [SerializeField] SkinnedMeshRenderer charRenderer;
    [SerializeField] private Rigidbody rb;


    public virtual void SetRandomColor()
    {
        ChangeColor((ColorType)Random.Range(0, colorRange));
    }
    public virtual void ChangeColor(ColorType color)
    {
        colorType = color;
        charRenderer.material = charData.GetMats(color);
    }

    protected virtual void OnInit()
    {

    }

    public void AddBrick()
    {
        brickAmount++;
        GameObject brick = ObjectPool.ins.GetPooledObject();
        brick.SetActive(true);
        brick.GetComponent<Brick>().ChangeColor(colorType);
        brick.transform.parent = bag.transform;
        brick.GetComponent<BoxCollider>().enabled = false;

        Vector3 offset = Vector3.up * GameConstants.BRICK_THICKNESS;
        if(brickStack.Count == 0)
        {
            Vector3 firstPosition = bag.transform.position;
            brick.transform.SetPositionAndRotation(firstPosition, bag.transform.rotation);
            brickStack.Push(brick);
        }
        else
        {
            Vector3 lastPosition = brickStack.Peek().transform.position;
            Vector3 newPosition = lastPosition + offset;


            brick.transform.SetPositionAndRotation(newPosition, brickStack.Peek().transform.rotation);
            //StartCoroutine(BrickFly(brick, newPosition));
            //brick.transform.rotation = bricks.Peek().transform.rotation;
            brickStack.Push(brick);
        }
    }

    public void PlaceBrick()
    {
        brickAmount--;
        brickStack.Peek().transform.parent = null;
        brickStack.Pop().SetActive(false);
    }

    private void  StandUp()
    {
        rb.isKinematic = false;
        isFalling = false;
        ChangeAnim(AnimName.IDLE);
    }

    private void DropAllBricks()
    {
        foreach (GameObject brick in brickStack)
        {
            brick.GetComponent<BoxCollider>().enabled = true;
            brick.GetComponent<Brick>().TurnOnPhysics();
            brick.transform.parent = null;
        }
        brickStack.Clear();
        brickAmount = 0;
    }


    private void Fall()
    {
        rb.isKinematic = true;
        isFalling = true;
        Invoke(nameof(StandUp), 2f);
        ChangeAnim(AnimName.FALL);
        Debug.Log(currentAnimName);
        DropAllBricks();
    }

    public void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(GameTag.CHARACTER))
        {
            if (brickAmount < collision.gameObject.GetComponent<Character>().brickAmount)
            {
                Fall();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(GameTag.WIN_ZONE))
        {
            ChangeAnim(AnimName.CHEER);
        }
    }
}