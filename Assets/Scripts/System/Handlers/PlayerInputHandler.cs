using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Car _playerCar;
    private bool _handbrakeEngaged = false;

    private void FixedUpdate()
    {
        HandleAcceleration();
        HandleSteering();
        HandleHandBrake();
    }

    private void HandleAcceleration()
    {
        float accelerationInput = _playerInput.actions["Accelerate"].ReadValue<float>();
        if ( accelerationInput > 0)
        {
            _playerCar.Accelerate(accelerationInput);
        }
        else
        {
            _playerCar.Decelerate(accelerationInput);
        }
    }

    private void HandleSteering()
    {
        float steerInput = _playerInput.actions["Steer"].ReadValue<float>();
        _playerCar.Steer(steerInput);
    }

    private void HandleHandBrake()
    {
        float handBrakeInput = _playerInput.actions["HandBrake"].ReadValue<float>();
        if (handBrakeInput == 1)
        {
            _playerCar.Handbrake();
        }
    }
}
