using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement ins;

    [SerializeField] private FloatingJoystick joystick;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private LayerMask stairLayer;
    [SerializeField] private Transform validStepChecker;
    private bool isMoving;


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
        rb.velocity = new Vector3(joystick.Horizontal * GameConstants.MOVE_SPEED * Time.deltaTime, rb.velocity.y , joystick.Vertical * GameConstants.MOVE_SPEED * Time.deltaTime);

        AdjustVelocityToSlope(rb.velocity);
        if(joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity);
        }
        
        if(!CheckValidStep())
        {
            rb.velocity = Vector3.zero;
        }
    }

    private void AdjustVelocityToSlope(Vector3 velocity)
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        if(Physics.Raycast(ray, out RaycastHit hit, GameConstants.RAYCAST_RANGE))
        {
            var slopeRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            var adjustedVelocity = slopeRotation * velocity;
            velocity = adjustedVelocity;
        }
    }

    private bool CheckValidStep()
    {
        Ray ray = new Ray(validStepChecker.position, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, GameConstants.RAYCAST_RANGE, stairLayer))
        {
            if (hit.transform.GetComponent<Step>().colorType == ColorType.No_Color)
            {
                Debug.Log(hit.transform.GetComponent<Step>().colorType);
                Debug.Log(isMoving);
                if (PlayerStacking.ins.bricksAmount == 0)
                    return (isMoving) ? PlayerStacking.ins.bricksAmount != 0 : PlayerStacking.ins.bricksAmount == 0;
            }
        }
        return isMoving;
    }
}
