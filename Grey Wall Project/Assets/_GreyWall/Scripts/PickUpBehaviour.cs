using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpBehaviour : MonoBehaviour
{
    // Componentes;
    private SpawnManager _spawnScript = null;
    [SerializeField]
    private GameObject _particle = null;

    // Variaveis;
    [SerializeField]
    private int _pickUpID = 0;
    [SerializeField]
    private int _scoreGiven = 1;

    private void Start()
    {
        if (_pickUpID == 1 || _pickUpID == 2)
            Destroy(this.gameObject, 20f);

        if (_pickUpID == 0 || _pickUpID == 3)
            if (GameObject.Find("PickUpSpawner").GetComponent<SpawnManager>() != null)
                _spawnScript = GameObject.Find("PickUpSpawner").GetComponent<SpawnManager>();
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0, 90, 0) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<PlayerController>() == null) return;

            PlayerController _playerScript = other.gameObject.GetComponent<PlayerController>();

            // Armas;
            if (_pickUpID == 0)
            {
                int _lastID = _playerScript._weaponID;
                _playerScript._weaponID = Random.Range(1, 5);

                if (_playerScript._weaponID == _lastID)
                {
                    _playerScript._weaponID++;
                    if (_playerScript._weaponID > 4) _playerScript._weaponID = 1;
                }

                _playerScript.ScoreMath(_scoreGiven);
                _playerScript.WeaponStats();
                _spawnScript.WeaponSpawn();
            }
            // Saude;
            else if (_pickUpID == 1)
            {
                _playerScript._currentHealth += 10;

                if (_playerScript._currentHealth > _playerScript._maxhealth)
                {
                    _playerScript._currentHealth = _playerScript._maxhealth;
                }

                _playerScript.ScoreMath(_scoreGiven);
            }
            // Municao;
            else if (_pickUpID == 2)
            {
                _playerScript._currentAmmo += (_playerScript._maxAmmo / 10);

                if (_playerScript._currentAmmo >= _playerScript._maxAmmo)
                {
                    _playerScript._currentAmmo = _playerScript._maxAmmo;
                }

                _playerScript.ScoreMath(_scoreGiven);
            }
            // PowerUps;
            else if (_pickUpID == 3)
            {
                //int _lastID = _playerScript._powerUpID;
                //int _randomPowerUp = Random.Range(0, 5);

                //if (_randomPowerUp == _lastID)
                //{
                //    _randomPowerUp++;
                //    if (_randomPowerUp > 4) _randomPowerUp = 0;
                //}

                //if (_randomPowerUp == 0)
                //{
                //    _playerScript.HighSpeedON();
                //}
                //else if (_randomPowerUp == 1)
                //{
                //    _playerScript.ShieldON();
                //}
                //else if (_randomPowerUp == 2)
                //{
                //    _playerScript.InfinityAmmoON();
                //}
                //else if (_randomPowerUp == 3)
                //{
                //    _playerScript.DecoyON();
                //}
                //else if (_randomPowerUp == 4)
                //{
                //    _playerScript.DoubleFireON();
                //}

                //_playerScript.ScoreMath(_scoreGiven);
                //_playerScript._powerUpID = _randomPowerUp;
                //_spawnScript.PowerUpSpawn();

                int _lastID = _playerScript._weaponID;
                _playerScript._weaponID = Random.Range(1, 5);

                if (_playerScript._weaponID == _lastID)
                {
                    _playerScript._weaponID++;
                    if (_playerScript._weaponID > 4) _playerScript._weaponID = 1;
                }

                _playerScript.ScoreMath(_scoreGiven);
                _playerScript.WeaponStats();
                _spawnScript.PowerUpSpawn();
            }

            Instantiate(_particle, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
