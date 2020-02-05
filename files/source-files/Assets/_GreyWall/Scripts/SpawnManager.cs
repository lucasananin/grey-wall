using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Referencias;
    [SerializeField] GameObject[] _objs = null;
    [SerializeField] GameObject[] _spawnPoints = null;
    [SerializeField] Light _mainLight = null;
    private AudioSource _hornAudio = null;

    // Valores;
    [Space]
    [SerializeField] int _spawnID = 0;
    [SerializeField] float _timer = 0f;
    [SerializeField] int _counter = 0;
    [Space]
    [SerializeField] float[] _enemyRepeatRates = null;
    public float _timeScale = 0f;
    private int _nextPowerUp = 0;
    private int _nextWeapon = 0;

    private void Start()
    {
        if (_spawnID == 0)
        {
            //_spawnPoints = GameObject.FindGameObjectsWithTag("EnemySpawner");
            _hornAudio = GetComponent<AudioSource>();
            _timeScale = Time.timeScale;
            StartCoroutine(Enemy0Routine(1f));
            //_enemyRepeatRates[0] -= (_enemyRepeatRates[0] * 0.25f);
            //_enemyRepeatRates[1] -= (_enemyRepeatRates[1] * 0.25f);
            //_enemyRepeatRates[2] -= (_enemyRepeatRates[2] * 0.25f);
            //_enemyRepeatRates[3] -= (_enemyRepeatRates[3] * 0.25f);
            //_enemyRepeatRates[4] -= (_enemyRepeatRates[4] * 0.25f);
            //StartCoroutine(Enemy0Routine(1f));
            //StartCoroutine(Enemy1Routine(1f));
            //StartCoroutine(Enemy2Routine(1f));
            //StartCoroutine(Enemy4Routine(1f));
            //StartCoroutine(Enemy5Routine(1f));
        }
        else
        {
            //_spawnPoints = GameObject.FindGameObjectsWithTag("PickUpSpawner");
            FirstWeapon();
            FirstPowerUp();
        }
    }

    private void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player") == null) return;

        _timer += Time.deltaTime;

        if (_timer >= 60f && _spawnID == 0)
        {
            _timer = 0f;
            _counter++;
            CheckWave();
        }
    }

    private void CheckWave()
    {
        if (_counter == 1)
        {
            StartCoroutine(Enemy1Routine(1f));
        }
        else if (_counter == 2)
        {
            StartCoroutine(Enemy2Routine(1f));
        }
        else if (_counter == 3)
        {
            StartCoroutine(Enemy4Routine(1f));
        }
        else if (_counter == 4)
        {
            StartCoroutine(Enemy5Routine(1f));
        }
        else if (_counter == 5)
        {
            _enemyRepeatRates[0] -= 0.1f;
        }
        else if (_counter == 6)
        {
            _enemyRepeatRates[1] -= 0.2f;
        }
        else if (_counter == 7)
        {
            _enemyRepeatRates[2] -= 0.3f;
        }
        else if (_counter == 8)
        {
            _enemyRepeatRates[3] -= 0.4f;
        }
        else if (_counter == 9)
        {
            _enemyRepeatRates[4] -= 0.5f;
            
            //if (_enemyRepeatRates[4] > 7.5f)
            //    _counter = 4;
        }
        else if (_counter == 10)
        {
            Time.timeScale += 0.1f;
            _timeScale = Time.timeScale;

            //if (Time.timeScale < 1.5f)
            //    _counter = 9;
        }
        else if (_counter == 11)
        {
            _mainLight.intensity -= 0.1f;

            if (_mainLight.intensity > 0.5f)
                _counter = 4;
        }
        else
        {
            return;
        }

        _hornAudio.Play();
    }

    private void FirstWeapon()
    {
        int _randomSpawn = Random.Range(0, 2);
        _nextWeapon = _randomSpawn;
        Instantiate(_objs[0], _spawnPoints[_randomSpawn].transform.position, Quaternion.identity);
    }

    private void FirstPowerUp()
    {
        int _randomSpawn = Random.Range(2, 4);
        _nextPowerUp = _randomSpawn;
        Instantiate(_objs[1], _spawnPoints[_randomSpawn].transform.position, Quaternion.identity);
    }

    public void WeaponSpawn()
    {
        _nextWeapon++;
        if (_nextWeapon > 1)
            _nextWeapon = 0;
        Instantiate(_objs[0], _spawnPoints[_nextWeapon].transform.position, Quaternion.identity);
    }

    public void PowerUpSpawn()
    {
        _nextPowerUp++;
        if (_nextPowerUp > 3)
            _nextPowerUp = 2;
        Instantiate(_objs[1], _spawnPoints[_nextPowerUp].transform.position, Quaternion.identity);
    }
    
    IEnumerator Enemy0Routine(float _timeToStart)
    {
        yield return new WaitForSeconds(_timeToStart);

        while (GameObject.FindGameObjectWithTag("Player") != null)
        {
            Transform _playerTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            bool _hasInstantiated = false;

            while (_hasInstantiated == false)
            {
                int _randomSpawn = Random.Range(0, _spawnPoints.Length);
                float _distance = Vector3.Distance(_spawnPoints[_randomSpawn].transform.position, _playerTarget.transform.position);

                if (_distance > 15)
                {
                    _hasInstantiated = true;
                    Instantiate(_objs[0], _spawnPoints[_randomSpawn].transform.position, Quaternion.identity);
                    yield return new WaitForSeconds(_enemyRepeatRates[0]);
                }
            }
        }
    }
    
    IEnumerator Enemy1Routine(float _timeToStart)
    {
        yield return new WaitForSeconds(_timeToStart);

        while (GameObject.FindGameObjectWithTag("Player") != null)
        {
            Transform _playerTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            bool _hasInstantiated = false;

            while (_hasInstantiated == false)
            {
                int _randomSpawn = Random.Range(0, _spawnPoints.Length);
                float _distance = Vector3.Distance(_spawnPoints[_randomSpawn].transform.position, _playerTarget.transform.position);

                if (_distance > 15)
                {
                    _hasInstantiated = true;
                    Instantiate(_objs[1], _spawnPoints[_randomSpawn].transform.position, Quaternion.identity);
                    yield return new WaitForSeconds(_enemyRepeatRates[1]);
                }
            }
        }
    }
    
    IEnumerator Enemy2Routine(float _timeToStart)
    {
        yield return new WaitForSeconds(_timeToStart);

        while (GameObject.FindGameObjectWithTag("Player") != null)
        {
            Transform _playerTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            bool _hasInstantiated = false;

            while (_hasInstantiated == false)
            {
                int _randomSpawn = Random.Range(0, _spawnPoints.Length);
                float _distance = Vector3.Distance(_spawnPoints[_randomSpawn].transform.position, _playerTarget.transform.position);

                if (_distance > 15)
                {
                    _hasInstantiated = true;
                    Instantiate(_objs[2], _spawnPoints[_randomSpawn].transform.position, Quaternion.identity);
                    yield return new WaitForSeconds(_enemyRepeatRates[2]);
                }
            }
        }
    }

    IEnumerator Enemy4Routine(float _timeToStart)
    {
        yield return new WaitForSeconds(_timeToStart);

        while (GameObject.FindGameObjectWithTag("Player") != null)
        {
            Transform _playerTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            bool _hasInstantiated = false;

            while (_hasInstantiated == false)
            {
                int _randomSpawn = Random.Range(0, _spawnPoints.Length);
                float _distance = Vector3.Distance(_spawnPoints[_randomSpawn].transform.position, _playerTarget.transform.position);

                if (_distance > 15)
                {
                    _hasInstantiated = true;
                    Instantiate(_objs[4], _spawnPoints[_randomSpawn].transform.position, Quaternion.identity);
                    yield return new WaitForSeconds(_enemyRepeatRates[3]);
                }
            }
        }
    }

    IEnumerator Enemy5Routine(float _timeToStart)
    {
        yield return new WaitForSeconds(_timeToStart);

        while (GameObject.FindGameObjectWithTag("Player") != null)
        {
            Transform _playerTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            bool _hasInstantiated = false;

            while (_hasInstantiated == false)
            {
                int _randomSpawn = Random.Range(0, _spawnPoints.Length);
                float _distance = Vector3.Distance(_spawnPoints[_randomSpawn].transform.position, _playerTarget.transform.position);

                if (_distance > 15)
                {
                    _hasInstantiated = true;
                    Instantiate(_objs[5], _spawnPoints[_randomSpawn].transform.position, Quaternion.identity);
                    yield return new WaitForSeconds(_enemyRepeatRates[4]);
                }
            }
        }
    }
}
