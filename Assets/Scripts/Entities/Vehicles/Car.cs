using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Car : MonoBehaviour
{ 
    [SerializeField] private Rigidbody _carRigidBody;
    [SerializeField] private SuspensionComponent _suspensionComponent;
    [SerializeField] private Transform _accelerationTransform;
    [SerializeField] private float _accelerationForce = 25.0f;
    [SerializeField] private float _backwardAccelerationForce = 10.0f;
    [SerializeField] private float _breakingForce = 30.0f;
    [SerializeField] private float _maxSpeed = 100.0f;
    [SerializeField] private float _steerStrenght = 15.0f;
    [SerializeField] private AnimationCurve _turningCurve;
    [SerializeField] private float _dragCoefitient = 1.0f;

    private Vector3 _currentCarLocalVelocity = Vector3.zero;
    private float _carVelocityRatio = 0.0f;

    public bool IsGrounded => _suspensionComponent.IsGrounded();

    private void Start()
    {
        _carRigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        ApplySuspension();
        ApplySidewaysDrag();
        UpdateVelocity();
    }

    public void Accelerate(float accelerationInput)
    {
        if (!IsGrounded) return;
        Vector3 force = _accelerationForce * accelerationInput * transform.forward;

        _carRigidBody.AddForceAtPosition(force, _accelerationTransform.position, ForceMode.Acceleration);
    }

    public void Decelerate(float deccelerationInput)
    {
        if (!IsGrounded) return;
        Vector3 force = _backwardAccelerationForce * deccelerationInput * transform.forward;

        _carRigidBody.AddForceAtPosition(force, _accelerationTransform.position, ForceMode.Acceleration);
    }
    public void Steer(float steerInput)
    {
        if (!IsGrounded) return;
        float steeringDirection = Mathf.Sign(Vector3.Dot(transform.forward, _carRigidBody.velocity));
        Vector3 torque = _steerStrenght * steerInput * _turningCurve.Evaluate(_carVelocityRatio) * steeringDirection * transform.up;

        _carRigidBody.AddTorque(torque, ForceMode.Acceleration);
    }

    public void Handbrake()
    {
        if (!IsGrounded) return;

        if (_carRigidBody.velocity.magnitude < 0.1f)
        {
            _carRigidBody.velocity = Vector3.zero;
            return;
        }

        Vector3 brakeDirection = -_carRigidBody.velocity.normalized;
        Vector3 brakeForceVector = brakeDirection * _breakingForce;

        _carRigidBody.AddForceAtPosition(brakeForceVector, _accelerationTransform.position, ForceMode.Acceleration);
    }

    private void ApplySuspension()
    {
        foreach (var suspensionPoint in _suspensionComponent.SuspensionPoints)
        {
            _suspensionComponent.ApplySuspension(suspensionPoint, _carRigidBody);
        }
    }

    private void ApplySidewaysDrag()
    {
        float currentSidewaysSpeed = _currentCarLocalVelocity.x;
        float dragMagnitude = -currentSidewaysSpeed * _dragCoefitient;

        Vector3 dragForce = transform.right * dragMagnitude;
        _carRigidBody.AddForceAtPosition(dragForce, _carRigidBody.worldCenterOfMass, ForceMode.Acceleration);
    }

    private void UpdateVelocity()
    {
        _currentCarLocalVelocity = transform.InverseTransformDirection(_carRigidBody.velocity);
        _carVelocityRatio = _currentCarLocalVelocity.z / _maxSpeed;
    }
}
