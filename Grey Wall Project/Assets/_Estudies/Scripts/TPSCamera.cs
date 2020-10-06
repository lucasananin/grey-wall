using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSCamera : MonoBehaviour
{
    // Referencias;
    private Transform _playerTarget = null;
    private Transform _pivotTarget = null;
    private Transform _cameraTarget = null;
    private Vector3 _positionVelocity = Vector3.zero;

    // Valores;
    [SerializeField] float _sensitivity = 5f;
    [SerializeField] float _distFromTarget = 5f;
    [SerializeField] float _scrollScale = 0.5f;
    [SerializeField] float _smoothRotTime = 20f;
    [SerializeField] float _smoothPosTime = 10f;
    [SerializeField] bool _isSmooth = true;
    private float _mouseH = 0f, _mouseV = 0f;

    private void Start()
    {
        _playerTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _pivotTarget = transform.GetChild(0);
        _cameraTarget = _pivotTarget.GetChild(0);
        transform.parent = null;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LateUpdate()
    {
        SetDistanceFromTarget();
        CameraRotation();
    }

    private void SetDistanceFromTarget()
    {
        if (Input.mouseScrollDelta != Vector2.zero)
        {
            print("ScrollRolled!");
            _distFromTarget -= Input.mouseScrollDelta.y * _scrollScale;
            _distFromTarget = Mathf.Clamp(_distFromTarget, 5, 10);
            _cameraTarget.position = _pivotTarget.position - _cameraTarget.forward * _distFromTarget;
        }
    }

    private void CameraRotation()
    {
        _mouseH += Input.GetAxis("Mouse X") * _sensitivity * 100 * Time.deltaTime;
        _mouseV -= Input.GetAxis("Mouse Y") * _sensitivity * 100 * Time.deltaTime;
        _mouseV = Mathf.Clamp(_mouseV, -75, 75);

        if (_isSmooth)
        {
            Quaternion _newRotH = Quaternion.Euler(0f, _mouseH, 0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, _newRotH, _smoothRotTime * Time.deltaTime);
            Quaternion _newRotV = Quaternion.Euler(_mouseV, 0f, 0f);
            _pivotTarget.localRotation = Quaternion.Slerp(_pivotTarget.localRotation, _newRotV, _smoothRotTime * Time.deltaTime);
            transform.position = Vector3.SmoothDamp(transform.position, _playerTarget.position, ref _positionVelocity, _smoothPosTime * Time.deltaTime);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, _mouseH, 0f);
            _pivotTarget.localRotation = Quaternion.Euler(_mouseV, 0f, 0f);
            transform.position = _playerTarget.position;
        }
    }
}
