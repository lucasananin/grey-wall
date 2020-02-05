using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    // Valores;
    [SerializeField] bool _isPaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            _isPaused = !_isPaused;
            Time.timeScale = (_isPaused) ? 0f : 1f;
        }
    }
}
