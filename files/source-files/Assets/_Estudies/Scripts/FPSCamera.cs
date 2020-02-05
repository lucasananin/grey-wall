using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCamera : MonoBehaviour
{
    // Referencias;
    private Transform _playerTarget = null;

    // Valores;
    [SerializeField] float _sensitivity = 5f;
    [SerializeField] float _smoothTime = 20f;
    [SerializeField] bool _isSmooth = true;
    private float _mouseH = 0f, _mouseV = 0f;

    private void Start()
    {
        _playerTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        _mouseH += Input.GetAxis("Mouse X") * _sensitivity * 100 * Time.deltaTime;
        _mouseV -= Input.GetAxis("Mouse Y") * _sensitivity * 100 * Time.deltaTime;
        _mouseV = Mathf.Clamp(_mouseV, -85, 85);

        if (_isSmooth)
        {
            Quaternion _newRotV = Quaternion.Euler(_mouseV, 0f, 0f);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, _newRotV, _smoothTime * Time.deltaTime);
            Quaternion _newPosH = Quaternion.Euler(0f, _mouseH, 0f);
            _playerTarget.localRotation = Quaternion.Slerp(_playerTarget.localRotation, _newPosH, _smoothTime * Time.deltaTime);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(_mouseV, 0f, 0f);
            _playerTarget.localRotation = Quaternion.Euler(0f, _mouseH, 0f);
        }
    }
}
