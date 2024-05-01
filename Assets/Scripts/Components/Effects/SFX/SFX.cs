using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Components.Effects.SFX
{
    [RequireComponent(typeof(AudioSource))]
    public class SFX : MonoBehaviour, ITogglableEffect
    {
        [SerializeField] protected AudioSource _audio;
        public void Toggle(bool active)
        {
            _audio.mute = !active;
        }
    }
}
