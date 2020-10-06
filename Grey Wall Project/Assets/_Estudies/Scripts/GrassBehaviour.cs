using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassBehaviour : MonoBehaviour
{
    // Referencias;
    [SerializeField] Vector3 _defaultSize = Vector3.zero;
    private Vector3 _newSize = Vector3.zero;

    // Valores;
    [SerializeField] float _smoothTime = 10f;
    [SerializeField] float _sizePercentage = 0.8f;
    [SerializeField] bool _hasCollided = false;
    [SerializeField] bool _isSmooth = true;

    private void Start()
    {
        transform.GetChild(0).localScale = _defaultSize;
        _newSize = _defaultSize * _sizePercentage;
        Rigidbody _rb = gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
        _rb.useGravity = false;
        _rb.isKinematic = true;
        BoxCollider _boxT = gameObject.AddComponent(typeof(BoxCollider)) as BoxCollider;
        _boxT.size = _defaultSize;
        _boxT.isTrigger = true;
        _boxT.center = new Vector3(0f, _defaultSize.y / 2f, 0f);
        StartCoroutine(InitialSizeRoutine());
    }

    private void Update()
    {
        if (_isSmooth)
        {
            if (_hasCollided)
            {
                if (transform.GetChild(0).lossyScale == _newSize) return;
                //print("ToNew!");
                transform.GetChild(0).localScale = Vector3.Lerp(transform.GetChild(0).localScale, _newSize, _smoothTime * Time.deltaTime);
            }
            else
            {
                if (transform.GetChild(0).lossyScale == _defaultSize) return;
                //print("ToDefault!");
                transform.GetChild(0).localScale = Vector3.Lerp(transform.GetChild(0).localScale, _defaultSize, _smoothTime * Time.deltaTime);
            }
        }
        else
        {
            if (_hasCollided)
            {
                //print("ToNew!");
                transform.GetChild(0).localScale = _newSize;
            }
            else
            {
                //print("ToDefault!");
                transform.GetChild(0).localScale = _defaultSize;
            }
        }
    }

    private IEnumerator InitialSizeRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        _hasCollided = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        //print("Enter!");
        _hasCollided = true;
    }

    private void OnTriggerExit(Collider other)
    {
        //print("Exit!");
        _hasCollided = false;
    }
}
