using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FirstPersonWeaponRotate : MonoBehaviour
{
    [SerializeField] Transform _mainCamera;
    [SerializeField] CinemachineBrain _brain;
    bool _isEnabled = true;

    private void Start()
    {
        _mainCamera = Camera.main.transform;
        _brain = _mainCamera.GetComponent<CinemachineBrain>();
    }
    // Update is called once per frame
    void Update()
    {
        if (!_isEnabled) return;

        transform.rotation = _mainCamera.rotation;
    }
}
