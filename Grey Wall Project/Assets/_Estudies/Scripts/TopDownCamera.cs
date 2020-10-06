using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCamera : MonoBehaviour
{
    // Referencias;
    private Transform _playerTarget = null;
    private Vector3 _positionVelocity = Vector3.zero;

    // Valores;
    [SerializeField] float _distanceFromTarget = 12f;
    [SerializeField] float _smoothTime = 20f;
    [SerializeField] bool _isSmooth = true;

    void Start()
    {
        _playerTarget = GameObject.FindGameObjectWithTag("Player").GetComponent(typeof(Transform)) as Transform;
        transform.parent = null;
    }

    void LateUpdate()
    {
        CameraMovement();
    }

    private void CameraMovement()
    {
        if (_playerTarget == null) return;

        if (_isSmooth)
        {
            Vector3 _newPos = _playerTarget.position - transform.forward * _distanceFromTarget;
            transform.position = Vector3.SmoothDamp(transform.position, _newPos, ref _positionVelocity, _smoothTime * Time.deltaTime);
        }
        else
        {
            transform.position = _playerTarget.position - transform.forward * _distanceFromTarget;
        }
    }
}
