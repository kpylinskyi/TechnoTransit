using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EngineSound : MonoBehaviour 
{
    [SerializeField] private AudioSource _audio;
    [SerializeField] private float _minPitch = 1.0f;
    [SerializeField] private float _maxPitch = 5.0f;

    public void SetPitch(float carVelocityRatio)
    {
        _audio.pitch = Mathf.Lerp(_minPitch, _maxPitch, Mathf.Abs(carVelocityRatio));
    }
}

