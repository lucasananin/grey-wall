using UnityEngine;

public class OronController : MonoBehaviour
{
    // Principais;
    public float speed = 1.0f;
    public float jumpSpeed = 1.0f;
    public float gravidade = 10.0f;
    public float rotationSpeed = 1000;

    // Classes;
    private Quaternion targetRotation;
    private Vector3 moveDirection;
    private Vector3 looktarget;

    // Componentes;
    private CharacterController controller;

    [SerializeField]
    private float _maxDash = 5f;
    [SerializeField]
    private float _dashDistance = 10f;
    float _dashStopping = 0.1f;
    float _currentDash = 0f;
    [SerializeField]
    private float _dashRate = 1f;
    private float _nextDash = 0f;
    private bool _canDoubleJump = true;

    void Start()
    {
        _currentDash = _maxDash;

        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        // We are grounded, so recalculate;
        // move direction directly from axes;
        looktarget = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), moveDirection.y, Input.GetAxisRaw("Vertical"));

        if (looktarget != Vector3.zero)
        {
            targetRotation = Quaternion.LookRotation(looktarget);
            transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetRotation.eulerAngles.y, rotationSpeed * Time.deltaTime);
            //moveDirection = transform.TransformDirection(moveDirection);
            //moveDirection = moveDirection * speed;
        }

        // Pulo;
        if (controller.isGrounded)
        {
            moveDirection.y = 0f;
            _canDoubleJump = true;

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Joystick1Button1))
            {
                moveDirection.y = jumpSpeed;
            }
        }

        // Pulo Duplo;
        if (!controller.isGrounded && Input.GetKeyDown(KeyCode.Space) && _canDoubleJump == true)
        {
            moveDirection.y = jumpSpeed;
            _canDoubleJump = false;
        }

        // Dash;
        if (Time.time > _nextDash)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _nextDash = Time.time + _dashRate;
                _currentDash = 0;
            }
        }

        if (_currentDash < _maxDash)
        {
            moveDirection = transform.forward * _dashDistance;
            _currentDash += _dashStopping;
        }

        // Apply gravity
        moveDirection.y -= gravidade * Time.deltaTime;

        // Move the controller
        controller.Move(moveDirection * Time.deltaTime * speed);
    }
}
