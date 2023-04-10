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

    private void Update()
    {
        PlayerMovement.ins.isMoving = !isFalling;
    }
    protected override void OnInit()
    {
        this.brickStack = new Stack<GameObject>();
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


}
