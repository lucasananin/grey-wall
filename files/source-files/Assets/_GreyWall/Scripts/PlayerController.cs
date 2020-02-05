using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Componentes;
    [SerializeField] ParticleSystem[] _weapons = null;
    [SerializeField] AudioClip[] _weaponClips = null;
    [SerializeField] GameObject _decoyPrefab = null;
    [SerializeField] GameObject _weapon2 = null;
    private CharacterController _cController;
    private Camera _topDownCam;
    private FadeManager _fadeManager = null;
    private UIManager _uiManager = null;
    private AudioSource _weaponAudio = null;

    // Variaveis;
    [Space]
    public float _moveSpeed = 1f;
    [SerializeField]
    private int _rotSpeed = 100;
    public int _maxhealth = 120;
    public int _currentHealth = 0;
    [Space]
    public int _weaponID = 0;
    [HideInInspector]
    public float _maxAmmo = 120;
    public float _currentAmmo = 0;
    public int _currentDamage = 10;
    [SerializeField]
    private float _fireRate = 0.1f;
    private float _nextFire = 0f;
    [Space]
    public int _powerUpID = 0;
    public int _totalScore = 0;
    private int _deathCounter = 0;
    [HideInInspector]
    public int _scoreMultiplier = 1;
    public int _powerPoints = 0;
    private int _floorMask;
    [HideInInspector]
    public bool _damaged = false;
    [Space]
    private bool _isShieldON = false;
    private bool _isInfinityAmmoON = false;
    public bool _canShoot = true;
    private bool _isDead = false;
    public bool _isShooting = false;

    private void Start()
    {
        _floorMask = LayerMask.GetMask("Floor");
        _cController = GetComponent<CharacterController>();
        _topDownCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        _fadeManager = GameObject.Find("FadeEffect").GetComponent<FadeManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _weaponAudio = GetComponentInChildren<AudioSource>();
        _currentHealth = _maxhealth;
        WeaponStats();
    }

    private void Update()
    {
        Movement();
        
        Rotation();

        Shoot();
        
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    _weaponID++;
        //    WeaponStats();

        //    if (_weaponID > 4)
        //    {
        //        _weaponID = 0;
        //        WeaponStats();
        //    }
        //}
    }

    private void Movement()
    {
        float _moveH = Input.GetAxisRaw("Horizontal");
        float _moveV = Input.GetAxisRaw("Vertical");

        Vector3 _moveDir = new Vector3(_moveH, 0f, _moveV);
        _moveDir = _moveDir.normalized * _moveSpeed;

        _moveDir.y -= 10f;
        _cController.Move(_moveDir * Time.deltaTime);
    }

    private void Rotation()
    {
        Ray _camRayLine = _topDownCam.ScreenPointToRay(Input.mousePosition);
        
        RaycastHit _hitInfo;
        
        if (Physics.Raycast (_camRayLine, out _hitInfo, 100f, _floorMask))
        {
            Vector3 _faceDir = _hitInfo.point - transform.position;
            
            _faceDir.y = 0f;
            
            Quaternion _newRot = Quaternion.LookRotation(_faceDir);
            
            transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y, _newRot.eulerAngles.y, _rotSpeed * Time.deltaTime);
        }
    }

    private void Shoot()
    {
        _nextFire += Time.deltaTime;

        if (Input.GetMouseButton(0) && _nextFire > _fireRate && _currentAmmo > 0 && _canShoot == true && _uiManager._isPaused == false)
        {
            _nextFire = 0f;
            _isShooting = true;
            _weaponAudio.PlayOneShot(_weaponClips[_weaponID]);

            if (_isInfinityAmmoON == false)
                _currentAmmo--;
            if (_weaponID != 2)
                _weapons[_weaponID].Play();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _isShooting = false;
        }
        
        if (_currentAmmo < 1 && _weaponID != 0)
        {
            _weaponID = 0;
            WeaponStats();
        }

        if (_weaponID == 0 && _isShooting == false)
        {
            PistoRecharge();
        }
    }

    public void WeaponStats()
    {
        if (_weaponID == 0)
        {
            _weapon2.SetActive(false);
            _fireRate = 0.2f;
            _currentDamage = 10;
            _maxAmmo = 60;
        }
        else if (_weaponID == 1)
        {
            _weapon2.SetActive(false);
            _fireRate = 0.1f;
            _currentDamage = 10;
            _maxAmmo = 120;
        }
        else if (_weaponID == 2)
        {
            _weapon2.SetActive(true);
            _fireRate = 0.3f;
            _currentDamage = 30;
            _maxAmmo = 40;
        }
        else if (_weaponID == 3)
        {
            _weapon2.SetActive(false);
            _fireRate = 0.4f;
            _currentDamage = 10;
            _maxAmmo = 30;
        }
        else if (_weaponID == 4)
        {
            _weapon2.SetActive(false);
            _fireRate = 0.1f;
            _currentDamage = 5;
            _maxAmmo = 90;
        }

        _currentAmmo = _maxAmmo;
    }

    private void PistoRecharge()
    {
        _currentAmmo += Time.deltaTime * 10;

        if (_currentAmmo > _maxAmmo)
        {
            _currentAmmo = _maxAmmo;
        }
    }

    public void DamageTaken(int _enemyDamage)
    {
        if (_isShieldON == true) return;

        _damaged = true;
        _currentHealth -= _enemyDamage;
        _scoreMultiplier = 1;
        _deathCounter = 0;

        if (_currentHealth < 1 && _isDead == false)
        {
            _isDead = true;
            Instantiate(_weapons[5], transform.position, transform.rotation);
            //_fadeManager.FadeToDie();
            //_uiManager.DeathScreen();
            //_uiManager.NewScoreScreen();
            SaveAndCheckScore();
            Time.timeScale = 1;
            Destroy(this.gameObject);
        }
    }

    private void SaveAndCheckScore()
    {
        //PlayerPrefs.SetInt("PlayerScore", _totalScore);

        if (_totalScore > PlayerPrefs.GetInt("FirstValue", 12000) || _totalScore > PlayerPrefs.GetInt("SecondValue", 6000) || _totalScore > PlayerPrefs.GetInt("ThirdValue", 3000))
        {
            _uiManager.NewScoreScreen();
        }
        else
        {
            _uiManager.DeathScreen();
        }
    }

    public void ScoreMath(int _enemyScore)
    {
        _deathCounter++;

        if (_deathCounter > _scoreMultiplier)
        {
            _scoreMultiplier++;
            _deathCounter = 0;
        }

        if (_powerPoints >= 100)
        {
            _powerPoints = 0;
            int _lastID = _powerUpID;
            int _randomPowerUp = Random.Range(0, 5);

            if (_randomPowerUp == _lastID)
            {
                _randomPowerUp++;
                if (_randomPowerUp > 4) _randomPowerUp = 0;
            }

            if (_randomPowerUp == 0)
            {
                HighSpeedON();
            }
            else if (_randomPowerUp == 1)
            {
                ShieldON();
            }
            else if (_randomPowerUp == 2)
            {
                InfinityAmmoON();
            }
            else if (_randomPowerUp == 3)
            {
                DecoyON();
            }
            else if (_randomPowerUp == 4)
            {
                DoubleFireON();
            }
            
            _powerUpID = _randomPowerUp;
        }

        _totalScore += _enemyScore * _scoreMultiplier;
        _powerPoints += (_enemyScore * 2);
    }

    public void HighSpeedON()
    {
        _weapons[6].Play();
        _moveSpeed += 2.5f;
        StartCoroutine(HighSpeedOFF());
    }

    IEnumerator HighSpeedOFF()
    {
        yield return new WaitForSeconds(10);
        _moveSpeed -= 2.5f;
        _weapons[6].Stop();
    }

    public void ShieldON()
    {
        _weapons[7].Play();
        _isShieldON = true;
        StartCoroutine(ShieldOFF());
    }

    IEnumerator ShieldOFF()
    {
        yield return new WaitForSeconds(10);
        _isShieldON = false;
        _weapons[7].Stop();
    }

    public void InfinityAmmoON()
    {
        _weapons[8].Play();
        _isInfinityAmmoON = true;
        StartCoroutine(InfinityAmmoOFF());
    }

    IEnumerator InfinityAmmoOFF()
    {
        yield return new WaitForSeconds(10);
        _isInfinityAmmoON = false;
        _weapons[8].Stop();
    }

    public void DecoyON()
    {
        GameObject _decoy = Instantiate(_decoyPrefab, transform.position, transform.rotation);
        _decoy.transform.parent = this.gameObject.transform;
        Destroy(_decoy, 10f);
    }

    public void DoubleFireON()
    {
        var _emission0 = _weapons[0].emission;
        _emission0.rateOverTime = 15;

        _emission0 = _weapons[1].emission;
        _emission0.rateOverTime = 15;

        _emission0 = _weapons[2].emission;
        _emission0.rateOverTime = 15;

        _emission0 = _weapons[3].emission;
        _emission0.rateOverTime = 45;

        _emission0 = _weapons[4].emission;
        _emission0.rateOverTime = 35;
        
        StartCoroutine(DoubleFireOFF());
    }

    IEnumerator DoubleFireOFF()
    {
        yield return new WaitForSeconds(10);

        var _emission0 = _weapons[0].emission;
        _emission0.rateOverTime = 10;

        _emission0 = _weapons[1].emission;
        _emission0.rateOverTime = 10;

        _emission0 = _weapons[2].emission;
        _emission0.rateOverTime = 10;

        _emission0 = _weapons[3].emission;
        _emission0.rateOverTime = 35;

        _emission0 = _weapons[4].emission;
        _emission0.rateOverTime = 25;
    }

    public void HealthRegenON()
    {
        _currentHealth = _maxhealth;
    }

    public void StunON()
    {
        _moveSpeed -= 1f;
        StartCoroutine(StunOFF());
    }

    IEnumerator StunOFF()
    {
        yield return new WaitForSeconds(2f);
        _moveSpeed += 1f;
    }

    public void DisarmedON()
    {
        _canShoot = false;
        StartCoroutine(DisarmedOFF());
    }

    IEnumerator DisarmedOFF()
    {
        yield return new WaitForSeconds(2);
        _canShoot = true;
    }
}
