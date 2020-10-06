using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoardEffect : MonoBehaviour
{
    // Referencias;
    private Transform _mainCam = null;

    private void Start()
    {
        _mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
    }

    private void Update()
    {
        transform.LookAt(_mainCam, Vector3.up);
    }
}
