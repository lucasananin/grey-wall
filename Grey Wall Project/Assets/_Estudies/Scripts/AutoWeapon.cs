using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoWeapon : MonoBehaviour
{
    // Referencias;
    [SerializeField] ParticleSystem _weaponPS = null;

    // Valores;
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
            _nextFire = 0;
            _weaponPS.Play();
        }
    }
}
