using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerActions;

[CreateAssetMenu(fileName = "New Input Reader", menuName = "Input/Input Reader")]
public class InputReader : ScriptableObject, IGameplayActions
{
    public event Action<Vector2> MovementEvent;
    public event Action TransformEvent;
    
    public Vector2 AimPosition { get; private set; }
    public Vector2 ScrollDelta { get; set; }
    
    public float BumperDelta { get; set; }

    private PlayerActions _playerActions;

    private void OnEnable()
    {
        if (_playerActions == null)
        {
            _playerActions = new PlayerActions();
            _playerActions.Gameplay.SetCallbacks(this);
        }
        
        _playerActions.Gameplay.Enable();
    }

    private void OnDisable()
    {
        _playerActions.Gameplay.Disable();
    }

    private void OnDestroy()
    {
        _playerActions.Gameplay.Disable();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        AimPosition = context.ReadValue<Vector2>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            MovementEvent?.Invoke(context.ReadValue<Vector2>());
        }
        else
        {
            MovementEvent?.Invoke(Vector2.zero);
        }
    }

    public void OnTransform(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            TransformEvent?.Invoke();
        }
    }

    public void OnMouseZoom(InputAction.CallbackContext context)
    {
        ScrollDelta = context.ReadValue<Vector2>();
    }

    public void OnControllerZoom(InputAction.CallbackContext context)
    {
        BumperDelta = context.ReadValue<float>();
    }
}
