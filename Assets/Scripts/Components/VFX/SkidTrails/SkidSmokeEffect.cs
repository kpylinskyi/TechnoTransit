using UnityEngine;

public class SkidSmokeEffect : SkidEffect
{
    [SerializeField] private ParticleSystem[] _skidSmokeParticleSystems = new ParticleSystem[2];

    public override void Toggle(bool active)
    {
        foreach (var skidSmokeParticleSystem in _skidSmokeParticleSystems)
        {
            if (active) skidSmokeParticleSystem.Play();
            else skidSmokeParticleSystem.Stop();
        }
    }
}
