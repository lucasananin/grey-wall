using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Referencias;
    private CharacterController _cController = null;

    // Valores;
    [Range(0, 100)]
    [SerializeField] int _maxHealth = 100;
    [Range(0, 100)]
    [SerializeField] int _currentHealth = 0;
    [SerializeField] float _fallDmgRes = 1f;
    private float _timeOnAir = 0f;
    private bool _isDead = false;

    private void Start()
    {
        _cController = GetComponent<CharacterController>();
        _currentHealth = _maxHealth;
    }

    private void Update()
    {
        FallDamage();
    }

    private void FallDamage()
    {
        if (!_cController.isGrounded)
        {
            _timeOnAir += Time.deltaTime;
        }
        else
        {
            if (_timeOnAir > _fallDmgRes)
                DamageTaken(10 * (int)_timeOnAir);

            _timeOnAir = 0f;
        }
    }

    public void DamageTaken(int _damageReceived)
    {
        if (_isDead) return;

        _currentHealth -= _damageReceived;

        if (_currentHealth < 1)
            Death();
    }

    private void Death()
    {
        _isDead = true;
        this.gameObject.SetActive(false);
    }
}
