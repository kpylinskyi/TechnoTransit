using UnityEngine;

public class SkidSmoke : MonoBehaviour, ITogglableEffect
{
    [SerializeField] private ParticleSystem[] _skidSmokeParticleSystems;

    public void Toggle(bool active)
    {
        foreach (var skidSmokeParticleSystem in _skidSmokeParticleSystems)
        {
            if (active) skidSmokeParticleSystem.Play();
            else skidSmokeParticleSystem.Stop();
        }
    }
}
