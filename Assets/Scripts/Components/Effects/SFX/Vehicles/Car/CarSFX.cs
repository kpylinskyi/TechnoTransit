using Assets.Scripts.Components.Effects.SFX.Vehicles.Car.Sounds;
using UnityEngine;

namespace Assets.Scripts.Components.Effects.SFX.Car
{
    public class CarSFX : MonoBehaviour
    {
        [SerializeField] private CarPawn _carPawn;

        [SerializeField] private EngineSound _engineSound;
        [SerializeField] private SFX _skidSound;
        [SerializeField] private float _skidSoundEmmitVelocity;

        private void OnEnable()
        {
            _carPawn.OnVelocityChanged.AddListener(AdjustEnginePitch);
            _carPawn.OnSkid.AddListener(ToggleSkidSound);
        }

        private void OnDisable() 
        {
            _carPawn.OnVelocityChanged?.RemoveListener(AdjustEnginePitch);
            _carPawn.OnSkid?.RemoveListener(ToggleSkidSound);
        }

        private void AdjustEnginePitch(float velocity) =>
            _engineSound.SetPitch(velocity);

        private void ToggleSkidSound(float velocity)
        {
            bool active = velocity >= _skidSoundEmmitVelocity;
            _skidSound.Toggle(active);
        }
    }
}
