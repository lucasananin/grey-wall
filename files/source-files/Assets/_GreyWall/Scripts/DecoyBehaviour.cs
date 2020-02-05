using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecoyBehaviour : MonoBehaviour
{
    // Componentes;
    private Transform _enemyTarget = null;
    private ParticleSystem _bullet = null;

    // Variaveis;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _nextFire = 0f;

    private void Start()
    {
        _bullet = GetComponent<ParticleSystem>();

        InvokeRepeating("Seek", 0f, 0.5f);
    }

    private void Update()
    {
        Shoot();
    }

    private void Seek()
    {
        GameObject[] _enemies = GameObject.FindGameObjectsWithTag("Enemy");

        float _shortestDistance = Mathf.Infinity;
        GameObject _nearestEnemy = null;

        foreach (GameObject _enemy in _enemies)
        {
            float _distanceToEnemy = Vector3.Distance(transform.position, _enemy.transform.position);

            if (_distanceToEnemy < _shortestDistance)
            {
                _shortestDistance = _distanceToEnemy;
                _nearestEnemy = _enemy;
            }
        }

        if (_nearestEnemy != null && _shortestDistance < 15f)
        {
            _enemyTarget = _nearestEnemy.transform;
        }
        else
        {
            _enemyTarget = null;
        }
    }

    private void Shoot()
    {
        _nextFire += Time.deltaTime;

        if (_enemyTarget != null && _nextFire > _fireRate)
        {
            _nextFire = 0f;
            transform.LookAt(_enemyTarget, Vector3.up);
            _bullet.Play();
        }
    }
}
