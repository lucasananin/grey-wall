using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampedValue : MonoBehaviour
{
    // Valores;
    [SerializeField] float _val = 0f;
    [SerializeField] float _intVal = 0f;
    [SerializeField] float _minVal = 0f;
    [SerializeField] float _maxVal = 10f;
    [SerializeField] bool _isIncreasing = false;

    private void Update()
    {
        _isIncreasing = Input.GetKey(KeyCode.Z);
        _val = (_isIncreasing) ? _val += Time.deltaTime : _val -= Time.deltaTime;
        _val = Mathf.Clamp(_val, _minVal, _maxVal);
        _intVal = (int)_val;
    }
}
