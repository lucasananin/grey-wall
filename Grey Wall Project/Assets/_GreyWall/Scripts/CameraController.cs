using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Componentes;
    private Transform _playerTarget;
    private Vector3 _offset;

    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            _playerTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

            _offset = transform.position - _playerTarget.transform.position;
        }
    }

    void LateUpdate()
    {
        if (_playerTarget == null) return;

        transform.position = _playerTarget.transform.position + _offset;
    }
}
