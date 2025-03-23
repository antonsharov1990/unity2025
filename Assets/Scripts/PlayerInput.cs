using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class CartControl : MonoBehaviour
{
    [SerializeField, Range(0, 1)]
    private float speed = 0.5f;

    [SerializeField, Range(0, 1)]
    private float gravity = 0.15f;

    private Vector2 moveInput = Vector2.zero;
    private Vector2 lookInput = Vector2.zero;
    private bool jumpInput = false;

    private CharacterController characterController;

    private Vector3 motion = Vector3.zero;
    private Vector3 lookDirection = Vector3.zero;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void OnMove(InputValue inputValue)
    {
        moveInput = inputValue.Get<Vector2>();
    }

    private void OnLook(InputValue inputValue)
    {
        lookInput = inputValue.Get<Vector2>();
    }

    private void FixedUpdate()
    {
        XYZMotionWithoutRigidBody();
        ViewRotationWithoutRigidBody();
    }

    private void XYZMotionWithoutRigidBody()
    {
        float gravityMovement = characterController.isGrounded ? 0 : gravity;
        //motion = new Vector3(moveInput.x * speed, -gravityMovement, moveInput.y * speed);
        //characterController.Move(motion);

        motion = new Vector3(moveInput.x * speed, 0, moveInput.y * speed);
        var localMotion = transform.rotation * motion;
        localMotion.y = -gravityMovement;

        if (jumpInput)
        {
            jumpInput = false;
            if (characterController.isGrounded)
            {
                localMotion.y = 2;
            }
        }


        characterController.Move(localMotion);
    }

    private void ViewRotationWithoutRigidBody()
    {
        lookDirection = transform.eulerAngles;
        lookDirection += new Vector3(-lookInput.y, lookInput.x, 0);
        lookDirection.z = 0;
        transform.rotation = Quaternion.Euler(lookDirection);

    }

    private void OnJump()
    {
        jumpInput = true;
    }

    

}