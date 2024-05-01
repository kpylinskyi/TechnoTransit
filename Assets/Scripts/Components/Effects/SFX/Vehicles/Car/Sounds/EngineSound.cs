using UnityEngine;


namespace Assets.Scripts.Components.Effects.SFX.Vehicles.Car.Sounds
{
    public class EngineSound : SFX, ITogglableEffect
    {
        [SerializeField] private float _minPitch = 1.0f;
        [SerializeField] private float _maxPitch = 5.0f;

        public void SetPitch(float carVelocityRatio)
        {
            _audio.pitch = Mathf.Lerp(_minPitch, _maxPitch, Mathf.Abs(carVelocityRatio));
        }
    }
}


