using System.Linq;
using UnityEngine;

public class CarSuspension : MonoBehaviour
{
    [SerializeField] private WheelHub[] _wheelSuspensionPoints;
    [SerializeField] private float _springStiffness;
    [SerializeField] private float _damperStiffness;
    [SerializeField] private float _restLength;
    [SerializeField] private float _springTravel;
    [SerializeField] private float _wheelRadius;

    public bool IsGrounded()
    {
        return _wheelSuspensionPoints.Any(p => p.IsGrounded);
    }

    public void SteerWheels(float steerInput)
    {
        foreach (var wheelSuspensionPoint in _wheelSuspensionPoints)
        {
            if (wheelSuspensionPoint is SteeringWheelHub steeringWheelSuspensionPoint)
            {
                steeringWheelSuspensionPoint.SteerWheel(steerInput);
            }
        }
    }

    public void RotateWheels(float accelerationInput, float carVelocityRatio)
    {
        foreach (var wheelSuspensionPoint in _wheelSuspensionPoints)
        {
            if (wheelSuspensionPoint is SteeringWheelHub steeringWheelSuspensionPoint)
            {
                steeringWheelSuspensionPoint.RotateWheel(carVelocityRatio);
            }
            else
            {
                wheelSuspensionPoint.RotateWheel(accelerationInput);
            }
        }
    }

    public void ApplySuspension(Rigidbody carRigidbody)
    {
        foreach (var wheelSuspensionPoint in _wheelSuspensionPoints)
        {
            float maxLenght = _restLength + _springTravel;
            if (Physics.Raycast(wheelSuspensionPoint.transform.position, -wheelSuspensionPoint.transform.up, out RaycastHit hit, maxLenght))
            {
                wheelSuspensionPoint.IsGrounded = true;

                float currentSpringLenght = hit.distance - _wheelRadius;
                float springCompretion = (_restLength - currentSpringLenght) / _springTravel;

                float springVelocity = Vector3.Dot(carRigidbody.GetPointVelocity(wheelSuspensionPoint.transform.position), wheelSuspensionPoint.transform.up);
                float dampForce = _damperStiffness * springVelocity;

                float springForce = _springStiffness * springCompretion;
                float netForce = springForce - dampForce;

                carRigidbody.AddForceAtPosition(wheelSuspensionPoint.transform.up * netForce, wheelSuspensionPoint.transform.position);

                Vector3 wheelPosition = hit.point + wheelSuspensionPoint.transform.up * _wheelRadius;
                wheelSuspensionPoint.SetWheelPosition(wheelPosition);

                Debug.DrawLine(wheelSuspensionPoint.transform.position, hit.point, Color.red);
            }
            else
            {
                wheelSuspensionPoint.IsGrounded = false;

                Vector3 wheelPosition = wheelSuspensionPoint.transform.position - wheelSuspensionPoint.transform.up * (maxLenght - _wheelRadius);
                wheelSuspensionPoint.SetWheelPosition(wheelPosition);

                Debug.DrawLine(wheelSuspensionPoint.transform.position, wheelSuspensionPoint.transform.position + (_wheelRadius + maxLenght) * -wheelSuspensionPoint.transform.up, Color.green);
            }
        }
    }
}
