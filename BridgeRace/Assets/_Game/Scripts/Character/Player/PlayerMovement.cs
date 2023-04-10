using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement ins;
    public bool isMoving;
    public Vector3 direction;

    [SerializeField] private Rigidbody rb;
    private float joystickRadius = 120f;
    private Vector3 firstMousePosition;
    private Vector3 currentMousePosition;
    private Vector3 BridgeDirection => GetComponent<Player>().bridge.bridgeDirection;
    public bool OnBridge => GetComponent<Player>().onBridge;

    [SerializeField] private GameObject joystick;
    [SerializeField] private GameObject joystickBackground;
    [SerializeField] private GameObject joystickHandle;
    [SerializeField] private LayerMask stairLayer;
    [SerializeField] private Transform validStepChecker;
    public bool isGround;
    private void Awake()
    {
        ins = this;
    }
    private void FixedUpdate()
    {
        CheckValidStep();
        isGround = CheckGround();
        Move();
    }

    private void Move()
    {
        if (!Player.ins.isFalling && joystick.activeSelf)
        {
            if (Input.GetMouseButtonDown(0))
            {
                firstMousePosition = Input.mousePosition;
                joystick.transform.position = firstMousePosition;
            }

            if (Input.GetMouseButton(0))
            {
                currentMousePosition = Input.mousePosition;
                direction = currentMousePosition - firstMousePosition;
                joystickHandle.transform.position = currentMousePosition;
                if (Vector3.Distance(joystickHandle.transform.position, joystickBackground.transform.position) > joystickRadius)
                {
                    joystickHandle.transform.position = joystickBackground.transform.position - (joystickBackground.transform.position - joystickHandle.transform.position).normalized * joystickRadius;
                }
                if (Vector3.Distance(joystickHandle.transform.position, joystickBackground.transform.position) > joystickRadius / 2)
                {
                    transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.y));
                    Player.ins.ChangeAnim(AnimName.RUN);
                    if (OnBridge && direction.y > 0.1f)
                    {
                        rb.velocity = new Vector3((float)direction.normalized.x, BridgeDirection.y, (float)direction.normalized.y) * GameConstants.CHAR_MOVE_SPEED;
                    }
                    else if (OnBridge && direction.y < 0.1f)
                    {
                        rb.velocity = new Vector3((float)direction.normalized.x, -BridgeDirection.y, (float)direction.normalized.y) * GameConstants.CHAR_MOVE_SPEED;
                    }
                    else
                    {
                        Vector3 newMovement = new Vector3(direction.normalized.x, rb.velocity.y, direction.normalized.y) * GameConstants.CHAR_MOVE_SPEED;
                        if (!isGround)
                        {
                            newMovement.y = -10f;
                        }
                        else
                        {
                            newMovement.y = 0f;
                        }
                        rb.velocity = newMovement;
                    }
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (!Player.ins.isFalling)
            {
                Player.ins.ChangeAnim(AnimName.IDLE);
            }
            joystick.transform.position += new Vector3(10000, 0, 0);
            rb.velocity = new Vector3(0, 0, 0);
        }
        if (!isMoving)
        {
            StopMoving();
        }
    }

    private bool CheckGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, GameConstants.RAYCAST_RANGE/4))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void StopMoving()
    {
        rb.velocity = Vector3.zero;
    }

    private void CheckValidStep()
    {
        Ray ray = new Ray(validStepChecker.position, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, GameConstants.RAYCAST_RANGE, stairLayer))
        {
            if (hit.transform.GetComponent<Step>().colorType != Player.ins.colorType)
            {
                if (Player.ins.brickAmount == 0)
                    isMoving = false;
                else
                    isMoving = true;
            }
        }
    }

}
