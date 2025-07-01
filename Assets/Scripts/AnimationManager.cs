using System;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    public bool isTransforming;
    public float playerSpeed;
    private Animator _animator;
    
    private readonly int _speedHash = Animator.StringToHash("Speed");
    private readonly int _isTransformedHash = Animator.StringToHash("isTransformed");
    private readonly int _transformHash = Animator.StringToHash("Transform");

    private void OnEnable()
    {
        inputReader.TransformEvent += TriggerTransform;
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    public void SetSpeedValue(float speed)
    {
        _animator.SetFloat(_speedHash, speed);
    }
    
    public void SetTransformedState(bool isTransformed)
    {
        _animator.SetBool(_isTransformedHash, isTransformed);
    }
    
    public void TriggerTransform()
    {
        _animator.SetTrigger(_transformHash);
    }

    public void SetTransforming(string state)
    {
        switch (state)
        {
            case "True":
                isTransforming = true;
                break;
            case "False":
                isTransforming = false;
                break;
        }
    }
}
