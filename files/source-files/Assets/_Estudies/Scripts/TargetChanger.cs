using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetChanger : MonoBehaviour
{
    // Referencias;
    [SerializeField] Transform[] _targets = null;

    // Valores;
    [SerializeField] float _distanceFromTarget = 5f;
    [SerializeField] float _smoothTime = 5f;
    [SerializeField] int _index = 0;

    void LateUpdate()
    {
        CameraMovement();
    }

    private void CameraMovement()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            _index++;
            if (_index >= _targets.Length)
                _index = 0;
        }

        Vector3 _newPosition = _targets[_index].position - transform.forward * _distanceFromTarget;
        transform.position = Vector3.Lerp(transform.position, _newPosition, _smoothTime * Time.deltaTime);
    }
}
