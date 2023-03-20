using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : UnityEngine.MonoBehaviour
{
    public static PlayerMovement ins;

    [SerializeField] private FloatingJoystick joystick;
    [SerializeField] private CharacterController controller;
    [SerializeField] private LayerMask stairLayer;
    [SerializeField] private Transform validStepChecker;
    private bool isMoving;
    private float inputX;
    private float inputZ;
    private float y;

    private void Awake()
    {
        ins = this;
    }
    private void Start()
    {
        OnInit();
    }

    public void OnInit()
    {
        isMoving = true;
    }
    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        inputX = joystick.Horizontal;
        inputZ = joystick.Vertical;
        if (controller.isGrounded)
        {
            y = 0f;
        }
        else
        {
            y += GameConstants.GRAVITY;
        }
        //movingVector = new Vector3(GameConstants.CHAR_MOVE_SPEED * inputX, tempY, GameConstants.CHAR_MOVE_SPEED * inputZ);
        controller.Move(Time.deltaTime * new Vector3(GameConstants.CHAR_MOVE_SPEED * inputX, y, GameConstants.CHAR_MOVE_SPEED * inputZ));

        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(GameConstants.CHAR_MOVE_SPEED * inputX, 0, GameConstants.CHAR_MOVE_SPEED * inputZ));
        }

        if (!CheckValidStep())
        {
            Debug.Log(isMoving);
            StopMoving();
        }
    }

    private void StopMoving()
    {
        controller.Move(Vector3.zero);
        Debug.Log(isMoving);
    }

    private bool CheckValidStep()
    {
        Ray ray = new Ray(validStepChecker.position, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, GameConstants.RAYCAST_RANGE, stairLayer))
        {
            if (hit.transform.GetComponent<Step>().colorType == ColorType.No_Color)
            {
                return (isMoving) ? Player.ins.brickAmount != 0 : Player.ins.brickAmount == 0;
            }
        }
        return isMoving;
    }

    /*private bool CheckGround()
    {
        return (Physics.Raycast(transform.position + Vector3.up * .2f, Vector3.down, GameConstants.RAYCAST_RANGE / 9f, groundLayer)) || (Physics.Raycast(transform.position + Vector3.up * .2f, Vector3.down, GameConstants.RAYCAST_RANGE / 5f, stairLayer));
    }*/
}
