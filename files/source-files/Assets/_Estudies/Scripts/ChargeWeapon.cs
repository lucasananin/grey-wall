using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeWeapon : MonoBehaviour
{
    // Referencias;
    [SerializeField] ParticleSystem _weaponPS = null;
    [SerializeField] ParticleSystem _loadingPS = null;
    [SerializeField] Color32 _loadingColor = Color.yellow;
    [SerializeField] Color32 _fullColor = Color.red;

    // Valores;
    [SerializeField] float _fireRate = 0.1f;
    private float _nextFire = 0f;
    private bool _canChangeToFull = true;
    private bool _canChangeToLoading = true;
    private bool _canLoad = true;

    public float _charger = 0;
    public float _chargeToShot = 1f;
    public bool _isCharging = false;

    private void Update()
    {
        Shoot();
    }

    private void Shoot()
    {
        _nextFire += Time.deltaTime;

        if (Input.GetMouseButton(0))
        {
            _charger += Time.deltaTime;

            if (_charger > _chargeToShot * 0.2f && _canLoad)
            {
                _canLoad = false;
                _loadingPS.Play();
            }

            _isCharging = true;
            SetColors();
        }
        else if (Input.GetMouseButtonUp(0) && _nextFire > _fireRate)
        {
            if (_charger > _chargeToShot)
                _canChangeToLoading = true;

            _charger = 0f;
            _canLoad = true;
            _canChangeToFull = true;
            _loadingPS.Stop();
            _weaponPS.Play();
        }
        else
        {
            _isCharging = false;
        }
    }

    private void SetColors()
    {
        if (_charger > _chargeToShot && _canChangeToFull)
        {
            _canChangeToFull = false;
            var _main = _weaponPS.main;
            _main.startSize = 0.8f;
            _main.startColor = new ParticleSystem.MinMaxGradient(_fullColor);
            _main = _loadingPS.main;
            _main.startSize = 0.2f;
            _main.startColor = new ParticleSystem.MinMaxGradient(_fullColor);
        }
        else if (_charger < _chargeToShot && _canChangeToLoading)
        {
            _canChangeToLoading = false;
            var _main = _weaponPS.main;
            _main.startSize = 0.4f;
            _main.startColor = new ParticleSystem.MinMaxGradient(_loadingColor);
            _main = _loadingPS.main;
            _main.startSize = 0.15f;
            _main.startColor = new ParticleSystem.MinMaxGradient(_loadingColor);
        }
    }
}
