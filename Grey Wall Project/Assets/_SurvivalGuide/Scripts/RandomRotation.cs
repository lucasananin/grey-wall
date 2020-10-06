using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotation : MonoBehaviour
{
    // Referencias;
    private Quaternion _newRot = Quaternion.identity;

    // Valores;
    [SerializeField] bool _isRotating = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !_isRotating)
        {
            //print("Pimbou!");
            float _x = Random.Range(0, 360);
            float _y = Random.Range(0, 360);
            float _z = Random.Range(0, 360);
            //transform.GetChild(0).rotation = Quaternion.Euler(_x, _y, _z);
            _newRot = Quaternion.Euler(_x, _y, _z);
            _isRotating = true;
            StartCoroutine(RotateRoutine());
        }
        else if (_isRotating)
        {
            //print("Rodando!");
            transform.GetChild(0).rotation = Quaternion.Slerp(transform.GetChild(0).rotation, _newRot, 10f * Time.deltaTime);
        }
    }

    private IEnumerator RotateRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        _isRotating = false;
    }
}
