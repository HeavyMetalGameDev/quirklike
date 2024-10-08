using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
public class CinemachinPOVExtension : CinemachineExtension
{
    private PlayerInputManager _playerInputManager;
    private Vector3 _startingRotation;

    [SerializeField]
    private float _xSensitivity = 10.0f;

    [SerializeField]
    private float _ySensitivity = 10.0f;

    [SerializeField]
    private float _clampAngle = 80.0f;
    protected override void Awake()
    {
        _playerInputManager = PlayerInputManager.Instance;
        base.Awake();
        if (_startingRotation == null) _startingRotation = transform.localRotation.eulerAngles;
    }
    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (vcam.Follow)
        {
            if(stage == CinemachineCore.Stage.Aim)
            {
                
                Vector2 deltaInput = _playerInputManager.GetMouseDelta();
                _startingRotation.x += deltaInput.x * Time.deltaTime * _ySensitivity;
                _startingRotation.y += deltaInput.y * Time.deltaTime * -_xSensitivity;
                _startingRotation.y = Mathf.Clamp(_startingRotation.y, -_clampAngle, _clampAngle);

                state.RawOrientation = Quaternion.Euler(_startingRotation.y, _startingRotation.x, 0.0f);
            }
        }
    }
}
