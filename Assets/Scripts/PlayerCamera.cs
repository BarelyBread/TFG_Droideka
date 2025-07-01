using System;
using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private float zoomSpeed = 2f;
    [SerializeField] private float zoomLerpSpeed = 10f;
    [SerializeField] private float minDistance = 3f;
    [SerializeField] private float maxDistance = 15f;

    [SerializeField] private InputReader inputReader;
    private CinemachineCamera _cam;
    private CinemachineOrbitalFollow _orbitalFollow;

    private float _targetZoom;
    private float _currentZoom;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;

        _cam = FindFirstObjectByType<CinemachineCamera>();
        _orbitalFollow = _cam.GetComponent<CinemachineOrbitalFollow>();

        _targetZoom = _currentZoom = _orbitalFollow.Radius;
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (inputReader.ScrollDelta.y != 0)
        {
            if (_orbitalFollow != null)
            {
                _targetZoom = Mathf.Clamp(_orbitalFollow.Radius - inputReader.ScrollDelta.y * zoomSpeed, minDistance, maxDistance);
                inputReader.ScrollDelta = Vector2.zero;
            }
        }

        float bumperDelta = inputReader.BumperDelta;
        if (bumperDelta != 0)
        {
            _targetZoom = Mathf.Clamp(_orbitalFollow.Radius - bumperDelta, minDistance, maxDistance);
        }
        
        _currentZoom = Mathf.Lerp(_currentZoom, _targetZoom, zoomLerpSpeed * Time.deltaTime);
        _orbitalFollow.Radius = _currentZoom;
    }
}
