using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    private DefaultPlayerActions _playerController;

    private static PlayerInputManager _instance;
    public static PlayerInputManager Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        _playerController = new DefaultPlayerActions();
    }

    private void OnEnable()
    {
        _playerController.Enable();
    }
    private void OnDisable()
    {
        _playerController.Disable();
    }

    public Vector2 GetPlayerMovement()
    {
        return _playerController.Player.Move.ReadValue<Vector2>();
    }

    public Vector2 GetMouseDelta()
    {
        return _playerController.Player.Look.ReadValue<Vector2>();
    }

    public bool PlayerFireHeld()
    {
        return _playerController.Player.Fire.WasPressedThisFrame();
    }

    public bool PlayerJumpPress()
    {
        return _playerController.Player.Jump.triggered;
    }
}
