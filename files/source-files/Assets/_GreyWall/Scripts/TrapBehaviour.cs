using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBehaviour : MonoBehaviour
{
    // Variaveis;
    [SerializeField]
    private int _trapID = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && _trapID == 0)
        {
            PlayerController _playerScript = other.gameObject.GetComponent<PlayerController>();

            if (_playerScript != null)
                _playerScript.DamageTaken(30);
        }
        else if (other.gameObject.CompareTag("Enemy") && _trapID == 1)
        {
            EnemyAI _enemyScript = other.gameObject.GetComponent<EnemyAI>();

            if (_enemyScript != null)
                _enemyScript.StunON();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && _trapID == 1)
        {
            EnemyAI _enemyScript = other.gameObject.GetComponent<EnemyAI>();

            if (_enemyScript != null)
                _enemyScript.StunOFF();
        }
    }
}
