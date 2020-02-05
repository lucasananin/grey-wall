using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampedPosition : MonoBehaviour
{
    // Valores;
    [SerializeField] float _moveSpeed = 5f;
    [SerializeField] float _minVal = -2f;
    [SerializeField] float _maxVal = 2f;

    private void Update()
    {
        Vector3 _moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        _moveDir = _moveDir.normalized * _moveSpeed;
        transform.Translate(_moveDir * Time.deltaTime);

        Vector3 _clampedPos = transform.position;
        _clampedPos.x = Mathf.Clamp(_clampedPos.x, _minVal, _maxVal);
        _clampedPos.z = Mathf.Clamp(_clampedPos.z, _minVal, _maxVal);
        transform.position = _clampedPos;
    }
}
