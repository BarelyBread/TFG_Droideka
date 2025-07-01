using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private bool faceMovementDirection;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private InputReader inputReader;

    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float rollSpeed = 7f;

    [SerializeField] private float velocity;

    private float _currentSpeed;
    private Rigidbody _rb;
    private CharacterController _controller;
    private AnimationManager _animationManager;

    private Vector2 _moveInput;
    private bool _isTransformed;

    public Vector3 moveDirection;

    public Vector3 forward;
    public Vector3 right;

    private void OnEnable()
    {
        inputReader.MovementEvent += HandleMoveInput;
        inputReader.TransformEvent += HandleTransformation;
    }

    private void OnDisable()
    {
        inputReader.MovementEvent -= HandleMoveInput;
        inputReader.TransformEvent -= HandleTransformation;
    }

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _animationManager = GetComponentInChildren<AnimationManager>();
    }

    void Start()
    {
        
    }
    
    void Update()
    {
        SetSpeed();
        MoveAndRotate();
        SetAnimSpeed();
    }

    private void SetSpeed()
    {
        if (_isTransformed)
        {
            _currentSpeed = rollSpeed;
        }
        else
        {
            _currentSpeed = walkSpeed;
        }
    }

    private void SetAnimSpeed()
    {
        velocity = _controller.velocity.sqrMagnitude;
        _animationManager.SetSpeedValue(velocity);
    }

    private void MoveAndRotate()
    {
        if (_animationManager.isTransforming) return;
        
        forward = cameraTransform.forward;
        right = cameraTransform.right;

        forward.y = 0;
        right.y = 0;
        
        forward.Normalize();
        right.Normalize();
        
        moveDirection.x = forward.x * _moveInput.y + right.x * _moveInput.x;
        moveDirection.z = forward.z * _moveInput.y + right.z * _moveInput.x;
        moveDirection.y = 0;
        
        _controller.Move(moveDirection * _currentSpeed * Time.deltaTime);

        if (faceMovementDirection && moveDirection.sqrMagnitude > 0.001f)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, 10f * Time.deltaTime);
        }
    }

    private void HandleMoveInput(Vector2 input)
    {
        _moveInput = input;
    }

    private void HandleTransformation()
    {
        if (_isTransformed == false)
        {
            _isTransformed = true;
            _animationManager.SetTransformedState(_isTransformed);
        }
        else
        {
            _isTransformed = false;
            _animationManager.SetTransformedState(_isTransformed);
        }
    }
}
