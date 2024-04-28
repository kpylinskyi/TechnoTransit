using UnityEngine;

[RequireComponent(typeof(AudioSource))]
internal class SkidSound : MonoBehaviour
{
    [SerializeField] private AudioSource _audio;

    public void Toggle(bool active)
    {
        _audio.mute = !active;
    }
}

