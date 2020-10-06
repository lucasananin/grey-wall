using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SemiBullet : MonoBehaviour
{
    // Referencias;
    //public GameObject _bulletHit = null;
    //public GameObject _hitPoint = null;
    private PlayerController _playerScript = null;

    // Valores;
    [SerializeField] float _moveSpeed = 5f;

    private void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            _playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        }

        Destroy(this.gameObject, 1f);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * _moveSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.tag != "Player")
        //{
        //    _bulletHit.SetActive(true);
        //    GameObject _gameObj = Instantiate(_bulletHit, _hitPoint.transform.position, transform.rotation);
        //    Destroy(_gameObj, 0.2f);
        //    Destroy(this.gameObject);
        //}

        if (other.gameObject.CompareTag("Enemy"))
        {
            if (other.gameObject.GetComponent<EnemyAI>() != null)
            {
                EnemyAI _enemyScript = other.gameObject.GetComponent<EnemyAI>();

                _enemyScript.DamageTaken(_playerScript._currentDamage);
            }
        }
        else if (other.gameObject.layer == 10)
        {
            Destroy(this.gameObject);
        }
    }
}
