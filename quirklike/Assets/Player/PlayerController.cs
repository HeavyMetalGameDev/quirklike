using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private DefaultPlayerActions _inputActions;
    private InputAction _moveAction;
    private InputAction _lookAction;
    private Rigidbody _rigidBody;
    private bool _isGrounded = false;

    private LayerMask _groundLayerMask;

    [SerializeField]
    private Transform _groundCheckTransform;

    [Range(0f, 100f)]
    public float _walkSpeed = 1.0f;

    [Range(0f, 100f)]
    public float _jumpImpulse = 5.0f;

    private void Awake()
    {
        _inputActions = new DefaultPlayerActions();
        _rigidBody = GetComponent<Rigidbody>();
        _groundLayerMask = LayerMask.GetMask("Ground");
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
        _isGrounded = Physics.Raycast(_groundCheckTransform.position, Vector3.down, 0.05f, _groundLayerMask);
        Vector2 movementDirection = _moveAction.ReadValue<Vector2>();
        Vector2 lookDirection = _lookAction.ReadValue<Vector2>();

        Vector3 currentVelocity = _rigidBody.velocity;

        currentVelocity.x = movementDirection.x * _walkSpeed;
        currentVelocity.z = movementDirection.y * _walkSpeed;
        _rigidBody.velocity = currentVelocity;
    }
    private void OnFire(InputAction.CallbackContext context)
    {

    }
    private void OnJump(InputAction.CallbackContext context)
    {
        if (_isGrounded)
            _rigidBody.AddForce(Vector3.up * _jumpImpulse, ForceMode.Impulse);
        
    }
    
}
