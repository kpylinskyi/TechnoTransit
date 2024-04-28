using UnityEngine;

public class SkidMarksEffect : SkidEffect
{
    [SerializeField] private TrailRenderer[] _skidMarkTrailRenderers = new TrailRenderer[2];
    public override void Toggle(bool active)
    {
        foreach (var skidMarkTrailRenderer in _skidMarkTrailRenderers)
        {
            skidMarkTrailRenderer.emitting = active;
        }
    }
}

