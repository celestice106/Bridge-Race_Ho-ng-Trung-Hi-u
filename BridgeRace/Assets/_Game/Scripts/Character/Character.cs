using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IColorChanging
{
    public int colorRange;

    public Stack<GameObject> brickStack;

    public Data charData;

    public ColorType colorType;

    public SkinnedMeshRenderer charRenderer;

    public string currentAnimName;

    public int brickAmount;

    [SerializeField] private Animator anim;

    [SerializeField] protected GameObject bag;
    public virtual void SetRandomColor()
    {
        ChangeColor((ColorType)Random.Range(0, colorRange));
    }
    public virtual void ChangeColor(ColorType color)
    {
        colorType = color;
        charRenderer.material = charData.GetMats(color);
    }

    public virtual void OnInit()
    {

    }

    public void AddBrick()
    {
        brickAmount++;
        GameObject brick = ObjectPool.ins.GetPooledObject();
        brick.SetActive(true);
        brick.GetComponent<Brick>().ChangeColor(colorType);
        Debug.Log(brick.GetComponent<Brick>().colorType);
        brick.transform.parent = bag.transform;

        Vector3 offset = Vector3.up * GameConstants.BRICK_THICKNESS;

        Vector3 lastPosition = brickStack.Peek().transform.position;
        Vector3 newPosition = lastPosition + offset;


        brick.transform.SetPositionAndRotation(newPosition, brickStack.Peek().transform.rotation);
        //StartCoroutine(BrickFly(brick, newPosition));
        //brick.transform.rotation = bricks.Peek().transform.rotation;
        brickStack.Push(brick);
    }

    public void PlaceBrick()
    {
        brickAmount--;
        brickStack.Peek().transform.parent = null;
        brickStack.Pop().SetActive(false);
    }




    /*private IEnumerator BrickFly(GameObject brick, Vector3 targetPosition)
    {
        brick.transform.position = Vector3.MoveTowards(brick.transform.position, targetPosition, GameConstants.BRICK_FLY_SPEED * Time.deltaTime);
        yield return new WaitForSeconds(GameConstants.BRICK_FLY_TIME);

    }*/

    protected void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }
}