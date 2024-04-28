using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Car : MonoBehaviour
{ 
    [SerializeField] private Rigidbody _carRigidBody;
    [SerializeField] private WheelSuspensionComponent _wheelSuspensionComponent;
    [SerializeField] private Transform _centerOfMassTransform;

    [SerializeField] private List<SkidEffect> _skidEffects;
    [SerializeField] private EngineSound _engineSound;
    [SerializeField] private SkidSound _skidSound;

    [SerializeField] private float _accelerationForce = 25.0f;
    [SerializeField] private float _backwardAccelerationForce = 10.0f;
    [SerializeField] private float _breakingDecelerationForce = 100.0f;
    [SerializeField] private float _breakingDragCoeffitient = 0.5f;
    [SerializeField] private float _maxSpeed = 100.0f;
    [SerializeField] private float _steerStrenght = 15.0f;
    [SerializeField] private AnimationCurve _turningCurve;
    [SerializeField] private float _dragCoeffitient = 1.0f;
    [SerializeField] private float _minSkidSideVelocity = 10.0f;

    private Vector3 _currentCarLocalVelocity = Vector3.zero;
    private float _carVelocityRatio = 0.0f;

    public bool IsGrounded => _wheelSuspensionComponent.IsGrounded();

    private void Start()
    {
        _carRigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        _wheelSuspensionComponent.ApplySuspension(_carRigidBody);
        ApplySidewaysDrag();
        UpdateVelocity();
    }

    private void Update()
    {
        ApplySkidEffects();
        _engineSound.SetPitch(_carVelocityRatio);
    }

    public void Accelerate(float accelerationInput)
    {
        _wheelSuspensionComponent.RotateWheels(accelerationInput, _carVelocityRatio);
        if (!IsGrounded) return;

        if (_currentCarLocalVelocity.z < _maxSpeed)
        {
            Vector3 force = _accelerationForce * accelerationInput * transform.forward;

            _carRigidBody.AddForceAtPosition(force, _centerOfMassTransform.position, ForceMode.Acceleration);
        }
    }

    public void Decelerate(float deccelerationInput)
    {
        _wheelSuspensionComponent.RotateWheels(deccelerationInput, _carVelocityRatio);
        if (!IsGrounded) return;
        Vector3 force = _backwardAccelerationForce * deccelerationInput * transform.forward;

        _carRigidBody.AddForceAtPosition(force, _centerOfMassTransform.position, ForceMode.Acceleration);
    }

    public void Steer(float steerInput)
    {
        _wheelSuspensionComponent.SteerWheels(steerInput);
        if (!IsGrounded) return;
        float steeringDirection = Mathf.Sign(Vector3.Dot(transform.forward, _carRigidBody.velocity));
        Vector3 torque = _steerStrenght * steerInput * _turningCurve.Evaluate(Mathf.Abs(_carVelocityRatio)) * steeringDirection * transform.up;

        _carRigidBody.AddTorque(torque, ForceMode.Acceleration);
    }

    public void Handbrake()
    {
        if (!IsGrounded) return;

        if (_carRigidBody.velocity.magnitude < 0.05f)
        {
            _carRigidBody.velocity = Vector3.zero;
            return;
        }

        Vector3 brakeDirection = -_carRigidBody.velocity.normalized;
        Vector3 brakeForceVector = brakeDirection * _breakingDecelerationForce;

        _carRigidBody.AddForceAtPosition(brakeForceVector, _centerOfMassTransform.position, ForceMode.Acceleration);
    }

    private void ApplySidewaysDrag()
    {
        float currentSidewaysSpeed = _currentCarLocalVelocity.x;
        float dragMagnitude = -currentSidewaysSpeed * _dragCoeffitient;

        Vector3 dragForce = transform.right * dragMagnitude;
        _carRigidBody.AddForceAtPosition(dragForce, _carRigidBody.worldCenterOfMass, ForceMode.Acceleration);
    }

    private void UpdateVelocity()
    {
        _currentCarLocalVelocity = transform.InverseTransformDirection(_carRigidBody.velocity);
        _carVelocityRatio = _currentCarLocalVelocity.z / _maxSpeed;
    }

    private void ApplySkidEffects()
    {
        if (IsGrounded && _carVelocityRatio > 0 && Mathf.Abs(_currentCarLocalVelocity.x) > _minSkidSideVelocity)
        {
            foreach(var effect in _skidEffects)
            {
                effect.Toggle(true);
                _skidSound.Toggle(true);
            }
        }
        else
        {
            foreach (var effect in _skidEffects)
            {
                effect.Toggle(false);
                effect.Toggle(false);
            }
        }
    }
}
