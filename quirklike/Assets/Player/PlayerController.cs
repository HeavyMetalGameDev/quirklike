using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private CharacterController _characterController;
    private PlayerInputManager _playerInputManager;
    //private Rigidbody _rigidBody;
    private bool _isGrounded = false;
    private LayerMask _groundLayerMask;
    private Vector3 _currentVelocity;

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
    }
    private void Update()
    {
        if (_isGrounded && _currentVelocity.y < 0)
            _currentVelocity.y = 0.0f;

        Vector2 movementValues = _playerInputManager.GetPlayerMovement();
        Vector3 move = new Vector3(movementValues.x, 0, movementValues.y);
        move = _cameraTransform.forward * move.z + _cameraTransform.right * move.x;
        move.y = 0.0f;

        _characterController.Move(move * Time.deltaTime * _walkSpeed);

        
        if (_isGrounded && _playerInputManager.PlayerJumpPress())
            Jump();

        _currentVelocity.y += _gravity * Time.deltaTime;
        _characterController.Move(_currentVelocity * Time.deltaTime);

    }

    private void FixedUpdate()
    {
        _isGrounded = _characterController.isGrounded;
    }

    private void Jump()
    {
        _currentVelocity.y += Mathf.Sqrt(_jumpHeight * -3.0f * _gravity);
    }
    private void Shoot()
    {

    }
}
