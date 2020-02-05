using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SemiWeapon : MonoBehaviour
{
    // Referencias;
    public GameObject _bulletPrefab = null;
    public GameObject _muzzle = null;
    private PlayerController _playerScript = null;

    // Valores;
    public float _fireRate = 0.1f;
    private float _nextFire = 0f;

    private void Start()
    {
        _playerScript = GameObject.FindGameObjectWithTag("Player").GetComponentInParent<PlayerController>();
    }

    private void Update()
    {
        _nextFire += Time.deltaTime;

        if (Input.GetMouseButton(0) && _nextFire > _fireRate && _playerScript._canShoot == true)
        {
            _nextFire = 0;
            Instantiate(_bulletPrefab, _muzzle.transform.position, transform.rotation);
        }
    }
}
