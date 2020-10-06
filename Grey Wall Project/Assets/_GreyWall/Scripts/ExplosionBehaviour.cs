using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBehaviour : MonoBehaviour
{
    // Componentes;
    private Collider _collider;
    [SerializeField] private AudioSource _audioSource = null;

    private void Start()
    {
        _collider = this.GetComponent<Collider>();

        _audioSource.Play();

        StartCoroutine(ColliderOFF());

        //Explode();

        Destroy(this.gameObject, 0.5f);
    }

    /*private void Explode()
    {
        Collider[] _collidersToDamage = Physics.OverlapSphere(transform.position, 2.5f);

        foreach(Collider _nearbyObject in _collidersToDamage)
        {
            PlayerController _playerScript = _nearbyObject.GetComponent<PlayerController>();

            if (_playerScript != null)
            {
                _playerScript.DamageTaken(60);
            }

            EnemyAI _enemyScript = _nearbyObject.GetComponent<EnemyAI>();

            if (_enemyScript != null)
            {
                _enemyScript.DamageTaken(60);
            }
        }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<PlayerController>() == null) return;

            PlayerController _playerScript = other.gameObject.GetComponent<PlayerController>();

            _playerScript.DamageTaken(60);
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            if (other.gameObject.GetComponent<EnemyAI>() == null) return;

            EnemyAI _enemyScript = other.gameObject.GetComponent<EnemyAI>();

            _enemyScript.DamageTaken(60);
        }
    }

    IEnumerator ColliderOFF()
    {
        yield return new WaitForSeconds(0.1f);
        _collider.enabled = false;
    }
}
