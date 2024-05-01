using UnityEngine;

public class SkidMarks : MonoBehaviour, ITogglableEffect
{
    [SerializeField] private TrailRenderer[] _skidMarkTrailRenderers;
    public void Toggle(bool active)
    {
        foreach (var skidMarkTrailRenderer in _skidMarkTrailRenderers)
        {
            skidMarkTrailRenderer.emitting = active;
        }
    }
}

