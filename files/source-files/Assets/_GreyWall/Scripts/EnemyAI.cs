using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    // Componentes;
    private NavMeshAgent _nav;
    private Transform _playerTarget;
    private PlayerController _playerScript;
    [SerializeField]
    private GameObject[] _drops = null;
    [SerializeField]
    private ParticleSystem[] _particles = null;
    [SerializeField] private AudioSource[] _audioSources = null;
    private Animator _anim = null;

    // Variaveis;
    [SerializeField]
    public int _enemyID = 0;
    public int _maxHealth = 10;
    public int _currentHealth = 10;
    public int _currentDamage = 10;
    [SerializeField]
    private float _attackRate = 1f;
    private float _nextAttack = 0f;
    [SerializeField]
    private float _attackRange = 2f;
    [SerializeField]
    private int _dropChance = 10;
    [SerializeField]
    private int _scoreGiven = 1;
    [Space]
    public bool _isPlayerInRange = false;
    private bool _isDead = false;
    private bool _isMoving = false;
    private bool _isAttacking = false;
    
    private void Start()
    {
        _currentHealth = _maxHealth;

        _nav = GetComponent<NavMeshAgent>();

        _anim = GetComponentInChildren<Animator>();

        _attackRange = _nav.stoppingDistance;

        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            _playerTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }

        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            _playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        }
    }

    private void Update()
    {
        Movement();

        Attack();
    }

    private void AnimationController(int val)
    {
        if (val == 0)
            _isMoving = false;
        else
            _isMoving = true;

        _anim.SetBool("isMoving", _isMoving);
    }

    private void Movement()
    {
        if (_playerTarget != null && Vector3.Distance(transform.position, _playerTarget.position) > _attackRange && _isAttacking == false)
        {
            _nav.isStopped = false;
            _nav.SetDestination(_playerTarget.position);
            AnimationController(1);
        }
        else
        {
            _nav.isStopped = true;
            AnimationController(0);
        }
    }

    private void Attack()
    {
        if (_playerScript == null) return;

        _nextAttack += Time.deltaTime;

        // Curto alcance;
        if (_enemyID == 0 || _enemyID == 4)
        {
            if (_nextAttack > _attackRate && _isPlayerInRange == true)
            {
                _nextAttack = 0f;
                if (_enemyID == 4) _playerScript.StunON();
                _playerScript.DamageTaken(_currentDamage);
                _isAttacking = true;
                _anim.SetTrigger("attack");
                StartCoroutine(AttackRoutine());
                _audioSources[1].Play();
                AnimationController(0);
            }
            else if (_nextAttack > _attackRate && _isPlayerInRange == false && Vector3.Distance(transform.position, _playerTarget.position) < 2)
            {
                Vector3 _faceDir = _playerTarget.position - transform.position;
                _faceDir.y = 0f;
                Quaternion _newRot = Quaternion.LookRotation(_faceDir);
                transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y, _newRot.eulerAngles.y, 300 * Time.deltaTime);
                AnimationController(1);
            }
        }
        // Longo alcance;
        else if (_enemyID == 1 || _enemyID == 5)
        {
            if (_nextAttack > _attackRate && _isPlayerInRange == true)
            {
                _nextAttack = 0f;
                _particles[2].Play();
                _isAttacking = true;
                _anim.SetTrigger("attack");
                StartCoroutine(AttackRoutine());
                _audioSources[1].Play();
                AnimationController(0);
            }
            else if (_nextAttack > _attackRate && _isPlayerInRange == false && Vector3.Distance(transform.position, _playerTarget.position) < 8)
            {
                Vector3 _faceDir = _playerTarget.position - transform.position;
                _faceDir.y = 0f;
                Quaternion _newRot = Quaternion.LookRotation(_faceDir);
                transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y, _newRot.eulerAngles.y, 300 * Time.deltaTime);
                AnimationController(1);
            }
        }
        // Explosivo;
        else if (_enemyID == 2)
        {
            if (_nextAttack > _attackRate && _isPlayerInRange == true)
            {
                Instantiate(_drops[2], transform.position + Vector3.up, transform.rotation);
                Destroy(this.gameObject);
            }
        }
        // Curador;
        else if (_enemyID == 3)
        {
            if (_nextAttack > _attackRate)
            {
                _nextAttack = 0f;

                Collider[] _enemiesToHeal = Physics.OverlapSphere(transform.position, 5f);

                foreach (Collider _nearbyEnemy in _enemiesToHeal)
                {
                    if (_nearbyEnemy.gameObject.CompareTag("Enemy"))
                    {
                        EnemyAI _enemyScript = _nearbyEnemy.GetComponent<EnemyAI>();

                        _enemyScript._currentHealth = _enemyScript._maxHealth;
                        //_enemyScript._currentHealth += 10;

                        /*if (_enemyScript._currentHealth > _enemyScript._maxHealth)
                        {
                            _enemyScript._currentHealth = _enemyScript._maxHealth;
                        }*/
                    }
                }
            }
        }
    }

    public void DamageTaken(int _playerDamage)
    {
        if (_isDead == true) return;

        _currentHealth -= _playerDamage;
        _audioSources[0].PlayOneShot(_audioSources[0].clip);
        _particles[0].Play();

        if (_currentHealth < 1 && _isDead == false)
        {
            if (_enemyID == 2)
            {
                _isDead = true;
                Instantiate(_drops[2], transform.position, transform.rotation);
                Instantiate(_drops[0], transform.position + Vector3.up, Quaternion.identity);
                _playerScript.ScoreMath(_scoreGiven);
                Destroy(this.gameObject);
            }
            else
            {
                _isDead = true;
                DropItem();
                _playerScript.ScoreMath(_scoreGiven);
                Instantiate(_particles[1], transform.position, transform.rotation);
                Destroy(this.gameObject);
            }
        }
    }

    private void DropItem()
    {
        int _randomDrop = Random.Range(0, _dropChance);

        if (_randomDrop == 0)
        {
            Instantiate(_drops[0], transform.position + Vector3.up, Quaternion.identity);
        }
        else if (_randomDrop == 1)
        {
            Instantiate(_drops[1], transform.position + Vector3.up, Quaternion.identity);
        }
    }

    public void StunON()
    {
        _nav.speed--;
    }

    public void StunOFF()
    {
        _nav.speed++;
    }

    IEnumerator AttackRoutine()
    {
        if (_isAttacking == true)
        {
            yield return new WaitForSeconds(0.4f);
            _isAttacking = false;
        }
    }
}
