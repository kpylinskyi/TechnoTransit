using UnityEngine;


public class SteeringWheelSuspension : WheelSuspension
{
    [SerializeField] private Transform _wheelSteeringPointTransform;
    [SerializeField] private float _maxWheelSteerAngle = 30.0f;

    public void SteerWheel(float steerInput)
    {
        if (_wheelSteeringPointTransform != null)
        {
            float steerAngle = steerInput * _maxWheelSteerAngle;

            var rotation = Quaternion.AngleAxis(steerAngle, Vector3.up);

            _wheelSteeringPointTransform.localRotation = rotation;
        }
        else
        {
            Debug.LogWarning("Wheel steering point transform not assigned in SteerWheelSuspensionPoint.");
        }
    }
}

