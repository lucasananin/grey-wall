using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookRot : MonoBehaviour
{
    // Referencias;
    [SerializeField] Transform _target = null;

    private void Update()
    {
        Vector3 _faceDir = _target.position - transform.position;
        _faceDir.y = 0f;
        Debug.DrawRay(transform.position, _faceDir, Color.red);
        Quaternion _newRot = Quaternion.LookRotation(_faceDir);
        transform.rotation = Quaternion.Slerp(transform.rotation, _newRot, 5f * Time.deltaTime);
    }
}
