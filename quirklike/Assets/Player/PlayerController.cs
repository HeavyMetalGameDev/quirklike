using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private CharacterController _characterController;
    private PlayerInputManager _playerInputManager;
    //private Rigidbody _rigidBody;
    private bool _isGrounded = false;
    private LayerMask _groundLayerMask;
    private Vector3 _currentVelocity;
    private float _maxRampAngle;
    private RaycastHit _slopeHit;

    [SerializeField]
    private Transform _groundCheckTransform;

    [SerializeField]
    [Range(0f, 100f)]
    public float _walkSpeed = 1.0f;

    [SerializeField]
    [Range(0f, 100f)]
    public float _jumpHeight = 5.0f;

    [SerializeField]
    [Range(-100f, -0f)]
    public float _gravity = -9.81f;

    private Transform _cameraTransform;

    private void Start()
    {
        _playerInputManager = PlayerInputManager.Instance;
        //_rigidBody = GetComponent<Rigidbody>();
        _characterController = GetComponent<CharacterController>();
        _groundLayerMask = LayerMask.GetMask("Ground");
        _currentVelocity = Vector3.zero;
        _cameraTransform = Camera.main.transform;
        Cursor.visible = false;
        _maxRampAngle = _characterController.slopeLimit;
    }
    private void Update()
    {

        bool onSlope = OnSlope();
        bool onSteepSlope = OnSteepSlope(); 
        if (_isGrounded && !onSteepSlope && !onSlope)
        {
            _currentVelocity.x = 0.0f;
            _currentVelocity.z = 0.0f;
        }
        if (_isGrounded && _currentVelocity.y < 0.0f && !onSlope)
        {
            _currentVelocity.y = 0.0f;
        }
            Vector2 movementValues = _playerInputManager.GetPlayerMovement();
        Vector3 move = new Vector3(movementValues.x, 0, movementValues.y);
        move = _cameraTransform.forward * move.z + _cameraTransform.right * move.x;

        move.y = 0.0f;
        
        
        if (!onSteepSlope && onSlope)
        {
            move = GetSlopeMoveDirection(move);
        }
        Debug.Log(move);
        _characterController.Move((move * _walkSpeed) * Time.deltaTime);
        ApplyGravity(onSteepSlope);
        
    }

    private void FixedUpdate()
    {
        _isGrounded = _characterController.isGrounded;
        
    }

    private void Jump()
    {
        ResetYSpeed();
        _currentVelocity.y += Mathf.Sqrt(_jumpHeight * -3.0f * _gravity);
    }
    private void Shoot()
    {

    }
    private void ApplyGravity(bool onSteepSlope)
    {

        _currentVelocity.y += _gravity * Time.deltaTime;
        if (_isGrounded && onSteepSlope)
        { 
            Debug.Log("STEEP SLOPE");
            _currentVelocity.x += (1f - _slopeHit.normal.y) * _slopeHit.normal.x * (1f - 0.5f);
            _currentVelocity.z += (1f - _slopeHit.normal.y) * _slopeHit.normal.z * (1f - 0.5f);
        }

        if (_isGrounded && !onSteepSlope && _playerInputManager.PlayerJumpPress())
        {
            Debug.Log("JUMP");
            Jump();
        }
        _characterController.Move(_currentVelocity * Time.deltaTime);
    }
  
    private void ResetYSpeed()
    {
        _currentVelocity.y = 0.0f;

    }
  
    private bool OnSlope()
    {
        if(Physics.Raycast(_groundCheckTransform.position, Vector3.down, out _slopeHit, 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, _slopeHit.normal);
            return angle < _maxRampAngle && angle != 0;
        }
        return false;
    }
    private bool OnSteepSlope()
    {
        if (Physics.Raycast(_groundCheckTransform.position, Vector3.down, out _slopeHit, 4.0f))
        {
            float angle = Vector3.Angle(Vector3.up, _slopeHit.normal);
            return angle >= _maxRampAngle;
        }
        return false;
    }

    private Vector3 GetSlopeMoveDirection(Vector3 moveDirection)
    {
        return Vector3.ProjectOnPlane(moveDirection, _slopeHit.normal).normalized;
    }

}
