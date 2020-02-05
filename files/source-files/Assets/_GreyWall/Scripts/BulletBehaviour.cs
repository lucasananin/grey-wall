using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    // Componentes;
    private PlayerController _playerScript;
    private EnemyAI _enemyScript;

    // Variaveis;
    [SerializeField]
    private int _bulletID = 0;
    [SerializeField]
    private int _decoyDamage = 10;

    private void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null && _bulletID == 0)
        {
            _playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        }
        
        if (gameObject.GetComponentInParent<EnemyAI>() != null && _bulletID == 2)
        {
            _enemyScript = gameObject.GetComponentInParent<EnemyAI>();
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        // Jogador;
        if (other.gameObject.CompareTag("Enemy") && _bulletID == 0)
        {
            if (other.gameObject.GetComponent<EnemyAI>() != null)
            {
                EnemyAI _enemyScript = other.gameObject.GetComponent<EnemyAI>();

                _enemyScript.DamageTaken(_playerScript._currentDamage);
            }
        }
        // Decoy;
        else if (other.gameObject.CompareTag("Enemy") && _bulletID == 1)
        {
            if (other.gameObject.GetComponent<EnemyAI>() != null)
            {
                EnemyAI _enemyScript = other.gameObject.GetComponent<EnemyAI>();

                _enemyScript.DamageTaken(_decoyDamage);
            }
        }
        // Inimigos;
        else if (other.gameObject.CompareTag("Player") && _bulletID == 2)
        {
            if (other.gameObject.GetComponent<PlayerController>() != null)
            {
                PlayerController _playerScript = other.gameObject.GetComponent<PlayerController>();

                _playerScript.DamageTaken(_enemyScript._currentDamage);

                if (_enemyScript._enemyID == 5)
                {
                    _playerScript.DisarmedON();
                }
            }
        }
    }
}
