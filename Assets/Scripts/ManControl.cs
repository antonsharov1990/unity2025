using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody))]
public class ManControl : MonoBehaviour
{
    [Header("Character movement stats")]
    [SerializeField, Range(0, 100)] private float _moveForce;
    [SerializeField, Range(0, 100)] private float _jumpForce;
    [SerializeField, Range(0, 100)] private float _rotateSpeed;

    [Header("Character components")]
    private Rigidbody _rigidbody;

    private Vector2 _moveInput;
    private Vector2 _lookInput;

    private bool _isGrounded;
    private bool _jumpInput;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _lookInput = Vector2.zero;
        _jumpInput = false;
        _isGrounded = false;
    }

    private void OnMove(InputValue inputValue)
    {
        Debug.Log("OnMove");
        _moveInput = inputValue.Get<Vector2>();
    }

    private void OnLook(InputValue inputValue)
    {
        Debug.Log("OnLook");
        _lookInput = inputValue.Get<Vector2>();
    }

    private void FixedUpdate()
    {
        XYZMotionWithRigidBody();
        BodyRotationToView();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if(collision.gameObject.CompareTag("Ground"))
        _isGrounded = true;
    }

    private void XYZMotionWithRigidBody()
    {
        Vector3 motion = new Vector3(_moveInput.x, 0, _moveInput.y);
        
        Vector3 localMotion = transform.rotation * motion;
        localMotion.y = 0;
        localMotion = localMotion.normalized * _moveForce;

        if (_jumpInput)
        {
            _jumpInput = false;
            if (_isGrounded)
            {
                _isGrounded = false;
                localMotion.y = _jumpForce;
            }
        }

        if (localMotion.magnitude <= 0)
        {
            return;
        }

        Debug.Log($"localMotion = {localMotion.magnitude}");

        //почему-то не работает гравитация
        Vector3 offset = localMotion * Time.deltaTime;
        _rigidbody.MovePosition(_rigidbody.position + offset); //двигается, но только если не поворачиваться.
        //_rigidbody.AddForce(localMotion * 1000, ForceMode.Force); //ни разу не двигается
    }

    private void BodyRotationToView()
    {
        Vector3 lookDirection = transform.eulerAngles;
        lookDirection += new Vector3(-_lookInput.y, _lookInput.x, 0);
        lookDirection.z = 0;
        transform.rotation = Quaternion.Euler(lookDirection);
    }

    private void OnJump()
    {
        _jumpInput = true;
    }

    

}