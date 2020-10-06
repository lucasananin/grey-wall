using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveCrate : MonoBehaviour
{
    // Referencias;
    [SerializeField] GameObject _crateInPieces = null;
    private BoxCollider _boxCol = null;

    // Valores;
    [SerializeField] float _explosionForce = 4f;
    [SerializeField] float _explosionRadius = 1f;

    private void Start()
    {
        _boxCol = GetComponent(typeof(BoxCollider)) as BoxCollider;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _boxCol.enabled = false;
            GameObject _obj = Instantiate(_crateInPieces, transform.position, Quaternion.identity);
            Rigidbody[] _rbs = _obj.GetComponentsInChildren<Rigidbody>();

            if (_rbs.Length > 0)
            {
                foreach (Rigidbody _rb in _rbs)
                {
                    _rb.AddExplosionForce(_explosionForce * 100f, transform.position, _explosionRadius);
                    //_rb.useGravity = false;
                }
            }

            Destroy(this.gameObject);
        }
    }
}
