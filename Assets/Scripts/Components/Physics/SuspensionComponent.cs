using Assets.Scripts.Components.Physics;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SuspensionComponent : MonoBehaviour
{
    public SuspensionPoint[] SuspensionPoints;
    [SerializeField] private float _springStiffness;
    [SerializeField] private float _damperStiffness;
    [SerializeField] private float _restLength;
    [SerializeField] private float _springTravel;
    [SerializeField] private float _wheelRadius;
    
    public bool IsGrounded()
    {
        return SuspensionPoints.Any(p => p.IsGrounded);
    }

    public void ApplySuspension(SuspensionPoint suspensionPoint, Rigidbody carRigidbody)
    {
        float maxLenght = _restLength + _springTravel;
        if (Physics.Raycast(suspensionPoint.transform.position, -suspensionPoint.transform.up, out RaycastHit hit, maxLenght))
        {
            suspensionPoint.IsGrounded = true;
            float currentSpringLenght = hit.distance - _wheelRadius;
            float springCompretion = (_restLength - currentSpringLenght) / _springTravel;

            float springVelocity = Vector3.Dot(carRigidbody.GetPointVelocity(suspensionPoint.transform.position), suspensionPoint.transform.up);
            float dampForce = _damperStiffness * springVelocity;

            float springForce = _springStiffness * springCompretion;
            float netForce = springForce - dampForce;

            carRigidbody.AddForceAtPosition(suspensionPoint.transform.up * netForce, suspensionPoint.transform.position);
            Debug.DrawLine(suspensionPoint.transform.position, hit.point, Color.red);
        }
        else
        {
            suspensionPoint.IsGrounded = false;
            Debug.DrawLine(suspensionPoint.transform.position, suspensionPoint.transform.position + (_wheelRadius + maxLenght) * -suspensionPoint.transform.up, Color.green);
        }
    } 
}
