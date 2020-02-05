using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateOnPoint : MonoBehaviour
{
    // Referencias;
    [SerializeField] GameObject _prefab = null;
    [SerializeField] Transform _point = null;

    // Valores;
    [SerializeField] bool _hasInstantiated = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _hasInstantiated == false)
        {
            GameObject _obj = Instantiate(_prefab, _point.transform.position, _point.transform.rotation);
            _obj.transform.parent = _point.transform;
            _hasInstantiated = true;
        }
    }
}
