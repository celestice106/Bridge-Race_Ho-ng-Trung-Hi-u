using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement ins;
    public bool isMoving;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private Joystick joystick;
    [SerializeField] private LayerMask stairLayer;
    [SerializeField] private Transform validStepChecker;

    private void Awake()
    {
        ins = this;
    }
    private void FixedUpdate()
    {
        CheckValidStep();
        Move();
    }

    private void Move()
    {
        float horizontal = joystick.InputHorizontal();
        float vertical = joystick.InputVertical();
        //Vector3 slopeForceDirection;
        Vector3 movement = new Vector3(horizontal, 0f, vertical);
        
        if (movement != Vector3.zero)
        {
            Player.ins.ChangeAnim(AnimName.RUN);
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 10f * Time.fixedDeltaTime);
            rb.velocity = GameConstants.CHAR_MOVE_SPEED * Time.deltaTime * movement;
        }
        /*RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, GameConstants.RAYCAST_RANGE))
        {
            if (hit.normal != Vector3.up)
            {
                slopeForceDirection = Vector3.Cross(Vector3.up, hit.normal.normalized);
                rb.velocity += slopeForceDirection *5f * Time.deltaTime;
            }
        }*/

        if (movement == Vector3.zero)
        {
            Player.ins.ChangeAnim(AnimName.IDLE);
        }
        if (!isMoving)
        {
            StopMoving();
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
            if (hit.transform.GetComponent<Step>().colorType == ColorType.No_Color)
            {
                if (Player.ins.brickAmount == 0)
                    isMoving = false;
                else
                    isMoving = true;
            }
        }
    }
}
