using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalReference : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
            GetTargetPosition();
    }

    private void GetTargetPosition()
    {
        // Cria uma variavel do tipo referencia local;
        Transform _targetPos = GameObject.Find("Target").GetComponent<Transform>();
        print("X: " + _targetPos.position.x + " Y: " + _targetPos.position.y + " Z: " + _targetPos.position.z);
    }
}
