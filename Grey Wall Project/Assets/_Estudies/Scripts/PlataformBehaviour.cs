using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformBehaviour : MonoBehaviour
{
    // Referencias;
    [SerializeField] Vector3 _pos1 = Vector3.zero;
    [SerializeField] Vector3 _pos2 = Vector3.zero;
    private Vector3 _nextPos = Vector3.zero;

    // Valores;
    [SerializeField] float _movSpeed = 1f;

    private void Start()
    {
        Rigidbody _rb = gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
        _rb.useGravity = false;
        _rb.isKinematic = true;
        BoxCollider _bC = gameObject.AddComponent(typeof(BoxCollider)) as BoxCollider;
        transform.position = _pos1;
    }

    private void Update()
    {
        if (transform.position == _pos1)
            _nextPos = _pos2;
        else if (transform.position == _pos2)
            _nextPos = _pos1;

        transform.position = Vector3.MoveTowards(transform.position, _nextPos, _movSpeed * Time.deltaTime);
    }
}
