using UnityEngine;
using UnityEngine.Events;

public class WheelHub : MonoBehaviour
{
    [SerializeField] private Transform _wheelTransform;
    [SerializeField] private float _tireRotationSpeed = 2500.0f;

    public UnityEvent OnSkid;
    public bool IsGrounded;

    public void RotateWheel(float accelerationInput)
    {
        if (_wheelTransform != null)
        {
            float rotationSpeed = _tireRotationSpeed * accelerationInput * Time.deltaTime;
            _wheelTransform.Rotate(Vector3.right, rotationSpeed, Space.Self);
        }
        else
        {
            Debug.LogWarning("Wheel transform not assigned in WheelSuspensionPoint.");
        }
    }

    public void SetWheelPosition(Vector3 wheelPosition)
    {
        _wheelTransform.position = wheelPosition;
    }
}

