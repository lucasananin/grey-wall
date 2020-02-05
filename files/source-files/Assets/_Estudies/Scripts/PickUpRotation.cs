using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpRotation : MonoBehaviour
{
    // Valores;
    [SerializeField] float _xSpeed = 15f;
    [SerializeField] float _ySpeed = 30f;
    [SerializeField] float _zSpeed = 45f;

    private void Start()
    {
        Rigidbody _rb = gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
        _rb.useGravity = false;
        _rb.isKinematic = true;
    }

    private void Update()
    {
        transform.Rotate(new Vector3(_xSpeed, _ySpeed, _zSpeed) * Time.deltaTime);
    }
}
