using UnityEditor;
using UnityEngine;
public class CarVFX: MonoBehaviour
{
    [SerializeField] private CarPawn _carPawn;

    [SerializeField] private SkidMarks _skidMarksEffect;
    [SerializeField] private float _skidMarksEmmitVelocity;

    [SerializeField] private SkidSmoke _skidSmokeEffect;
    [SerializeField] private float _skidSmokeEmmitVelocity;

    private void OnEnable()
    {
        _carPawn.OnSkid.AddListener(ToggleSkidMarksEffect);
        _carPawn.OnSkid.AddListener(ToggleSkidSmokeEffect);
        // _carPawn.OnHandBrake.AddListener(ToggleSkidSmokeEffect);
    }

    private void OnDisable()
    {
        _carPawn.OnSkid?.RemoveListener(ToggleSkidMarksEffect);
        _carPawn.OnSkid?.RemoveListener(ToggleSkidSmokeEffect);
        // _carPawn.OnHandBrake?.RemoveListener(ToggleSkidSmokeEffect);
    }

    private void ToggleSkidMarksEffect(float sideVelocity)
    {
        bool active = Mathf.Abs(sideVelocity) >= _skidMarksEmmitVelocity;
        _skidMarksEffect.Toggle(active);
    }
        
    public void ToggleSkidSmokeEffect(float sideVelocity)
    {
        bool active = Mathf.Abs(sideVelocity) >= _skidSmokeEmmitVelocity;
        _skidSmokeEffect.Toggle(active);
    }
        
}

