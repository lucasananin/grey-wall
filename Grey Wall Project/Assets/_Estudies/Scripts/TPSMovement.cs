using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSMovement : MonoBehaviour
{
    // Referencias;
    private CharacterController _cController = null;
    private Animator _anim = null;
    private Transform _tpsCam = null;
    private Vector3 _moveDir = Vector3.zero;

    // Valores;
    [SerializeField] float _walkSpeed = 1f;
    [SerializeField] float _runSpeed = 2f;
    [SerializeField] float _rotSpeed = 10f;
    [SerializeField] float _gravity = 20f;
    [SerializeField] float _jumpHeight = 2f;
    [SerializeField] float _maxJump = 2f;
    [SerializeField] float _dashSpeed = 20f;
    [SerializeField] float _dashRate = 1f;
    [SerializeField] float _maxDash = 0.2f;
    [SerializeField] bool _isMoving = false;
    [SerializeField] bool _isRunning = false;
    [SerializeField] bool _isSmooth = false;
    [SerializeField] bool _isDashing = false;
    private float _moveSpeed = 0f;
    //private float _rotationVelocity = 0f;
    private float _verticalDir = 0f;
    private float _currentJump = 0f;
    private float _currentDash = 0f;
    private float _nextDash = 0f;

    private void Start()
    {
        _cController = GetComponent<CharacterController>();
        _anim = GetComponentInChildren<Animator>();
        _tpsCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
        _dashRate += _maxDash;
        _nextDash = _dashRate;
        _currentDash = _maxDash;
    }

    private void Update()
    {
        Movement();
        Animating();
    }

    private void Animating()
    {
        _isMoving = _moveDir.x != 0f || _moveDir.z != 0f;
        _anim.SetBool("isMoving", _isMoving);
    }

    private void Movement()
    {
        if (!_isDashing)
        {
            _moveDir.Set(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
            _moveDir = _moveDir.normalized * _moveSpeed;
            _moveDir = transform.TransformDirection(_moveDir);

            if (_moveDir != Vector3.zero)
            {
                if (_isSmooth)
                {
                    Quaternion _newRot = Quaternion.Euler(0f, _tpsCam.eulerAngles.y, 0f);
                    transform.rotation = Quaternion.Slerp(transform.rotation, _newRot, _rotSpeed * Time.deltaTime);
                    //transform.eulerAngles = transform.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, _tpsCam.transform.eulerAngles.y, ref _rotationVelocity, _rotSpeed * Time.deltaTime);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0f, _tpsCam.eulerAngles.y, 0f);
                }
            }

            RunAction();
            JumpAction();
            _verticalDir -= (_gravity * Time.deltaTime);
            _moveDir += Vector3.up * _verticalDir;
        }

        DashAction();
        _cController.Move(_moveDir * Time.deltaTime);
    }

    private void RunAction()
    {
        if (!_cController.isGrounded) return;
        _isRunning = Input.GetKey(KeyCode.LeftShift);
        _moveSpeed = (_isRunning) ? _runSpeed : _walkSpeed;
    }

    private void JumpAction()
    {
        if (_cController.isGrounded)
        {
            _verticalDir = 0f;
            _currentJump = 0f;
        }

        if (Input.GetKeyDown(KeyCode.Space) && _currentJump < _maxJump)
        {
            float _jumpDir = Mathf.Sqrt(-2 * -_gravity * _jumpHeight);
            _verticalDir = _jumpDir;
            _currentJump++;
        }
    }

    private void DashAction()
    {
        _nextDash += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.LeftControl) && _nextDash > _dashRate && _isDashing == false)
        {
            _isDashing = true;
            _nextDash = 0f;
            _currentDash = 0f;
        }

        if (_currentDash < _maxDash)
        {
            _currentDash += Time.deltaTime;
            _verticalDir = 0f;
            _moveDir.y = 0f;

            if (_moveDir.x != 0 || _moveDir.z != 0)
                _moveDir = _moveDir.normalized * _dashSpeed;
            else
                _moveDir = -transform.forward * _dashSpeed;
        }
        else
        {
            _isDashing = false;
            _currentDash = _maxDash;
        }
    }
}
