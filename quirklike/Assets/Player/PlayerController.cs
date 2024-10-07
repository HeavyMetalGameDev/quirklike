using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private DefaultPlayerActions _inputActions;
    private InputAction _moveAction;
    private InputAction _lookAction;

    private void Awake()
    {
        _inputActions = new DefaultPlayerActions();
    }

    private void OnEnable()
    {
        _moveAction = _inputActions.Player.Move;
        _inputActions.Player.Move.Enable();
        _lookAction = _inputActions.Player.Look;
        _inputActions.Player.Look.Enable();

        _inputActions.Player.Fire.performed += OnFire;
        _inputActions.Player.Fire.Enable();


        _inputActions.Player.Jump.performed += OnJump;
        _inputActions.Player.Jump.Enable();
    }

    private void OnDisable()
    {
        _moveAction.Disable();
        _lookAction.Disable();

        _inputActions.Player.Fire.performed -= OnFire;
        _inputActions.Player.Fire.Disable();

        _inputActions.Player.Jump.performed -= OnJump;
        _inputActions.Player.Jump.Disable();
    }

    private void FixedUpdate()
    {
        Vector2 movementDirection = _moveAction.ReadValue<Vector2>();
        Vector2 lookDirection = _lookAction.ReadValue<Vector2>();
    }
    private void OnFire(InputAction.CallbackContext context)
    {

    }
    private void OnJump(InputAction.CallbackContext context)
    {

    }
    
}
