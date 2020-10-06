using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserWeapon : MonoBehaviour
{
    // Componentes;
    [SerializeField] GameObject _muzzle = null;
    [SerializeField] GameObject _bulletHit = null;
    [SerializeField] LineRenderer _laserLine = null;

    // Variaveis;
    [SerializeField] float _fireRate = 0.1f;
    private float _nextFire = 0f;

    private void Update()
    {
        Shoot();
    }

    private void Shoot()
    {
        _nextFire += Time.deltaTime;

        if (Input.GetMouseButton(0) && _nextFire > _fireRate)
        {
            _nextFire = 0f;

            Ray _shootRay = new Ray(_muzzle.transform.position, _muzzle.transform.forward);
            RaycastHit _shootHit;

            if (Physics.Raycast(_shootRay, out _shootHit, 20f))
            {
                GameObject _bulletPS = Instantiate(_bulletHit, _shootHit.point, Quaternion.identity);
                Destroy(_bulletPS, 0.2f);
                Debug.Log(_shootHit.transform.name);
            }
        }
        else if (Input.GetMouseButton(0))
        {
            Ray _laserRay = new Ray(_muzzle.transform.position, _muzzle.transform.forward);
            RaycastHit _laserHit;

            _laserLine.SetPosition(0, _muzzle.transform.position);

            if (!_laserLine.enabled)
                _laserLine.enabled = true;

            if (Physics.Raycast(_laserRay, out _laserHit, 20f))
            {
                _laserLine.SetPosition(1, _laserHit.point);
            }
            else
            {
                _laserLine.SetPosition(1, _laserRay.origin + _laserRay.direction * 20f);
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _laserLine.enabled = false;
        }
    }
}
