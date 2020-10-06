using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownMovement : MonoBehaviour
{
    // Referencias;
    private CharacterController _cController = null;
    private Animator _anim = null;
    private Camera _topDownCam = null;
    private GameObject _aimLine = null;
    private Vector3 _moveDir = Vector3.zero;

    // Valores;
    [SerializeField] float _walkSpeed = 5f;
    [SerializeField] float _runSpeed = 10f;
    [SerializeField] float _rotSpeed = 10f;
    [SerializeField] float _gravity = 20f;
    [SerializeField] float _jumpHeight = 2f;
    [SerializeField] float _maxJump = 2f;
    [SerializeField] float _dashSpeed = 20f;
    [SerializeField] float _dashRate = 1f;
    [SerializeField] float _maxDash = 0.2f;
    [SerializeField] bool _isMoving = false;
    [SerializeField] bool _isAiming = false;
    [SerializeField] bool _isRunning = false;
    [SerializeField] bool _isDashing = false;
    private int _floorMask = 0;
    private float _moveSpeed = 0f;
    private float _verticalDir = 0f;
    private float _currentJump = 0f;
    private float _currentDash = 0f;
    private float _nextDash = 0f;

    // Internos;
    private void Start()
    {
        _floorMask = LayerMask.GetMask("Floor");
        _cController = GetComponent(typeof(CharacterController)) as CharacterController;
        _anim = GetComponentInChildren(typeof(Animator)) as Animator;
        _topDownCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent(typeof(Camera)) as Camera;
        _aimLine = GameObject.Find("AimLine");
        _aimLine.SetActive(false);
        _dashRate += _maxDash;
        _nextDash = _dashRate;
        _currentDash = _maxDash;
    }
    
    private void Update()
    {
        ChangeStance();
        FreeMovement();
        Animating();
    }

    // Personalizados;
    private void Animating()
    {
        _isMoving = _moveDir.x != 0f || _moveDir.z != 0f;
        _anim.SetBool("isMoving", _isMoving);
    }

    private void ChangeStance()
    {
        if (!Input.GetMouseButtonDown(1)) return;
        _isAiming = !_isAiming;
        _aimLine.SetActive(_isAiming);
    }

    private void FreeMovement()
    {
        if (!_isDashing)
        {
            _moveDir.Set(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
            _moveDir = _moveDir.normalized * _moveSpeed;

            AimingRotation();
            RunAction();
            JumpAction();

            _verticalDir -= (_gravity * Time.deltaTime);
            _moveDir += Vector3.up * _verticalDir;
        }

        DashAction();
        _cController.Move(_moveDir * Time.deltaTime);
    }

    private void AimingRotation()
    {
        if (!_isAiming)
        {
            if (_moveDir != Vector3.zero)
            {
                Quaternion _newRot = Quaternion.LookRotation(_moveDir, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, _newRot, _rotSpeed * Time.deltaTime);
            }
        }
        else
        {
            Ray _camRayLine = _topDownCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit _hitInfo;

            if (Physics.Raycast(_camRayLine, out _hitInfo, 50f, _floorMask))
            {
                Vector3 _faceDir = _hitInfo.point - transform.position;
                _faceDir.y = 0f;
                Quaternion _newRot = Quaternion.LookRotation(_faceDir, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, _newRot, _rotSpeed * Time.deltaTime);
            }
        }
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

        if (Input.GetKeyDown(KeyCode.LeftControl) && _nextDash > _dashRate && !_isDashing)
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
                _moveDir = transform.forward * _dashSpeed;
        }
        else
        {
            _isDashing = false;
            _currentDash = _maxDash;
        }
    }
}
