using System.Collections.Generic;
using UnityEngine;

namespace Sound
{
    public class SoundManager : ASinglton<SoundManager>
    {
        [SerializeField] private Dictionary<SoundTypes, AudioSource> _sources = new Dictionary<SoundTypes, AudioSource>();

        private float _volumeSounds;

        protected override SoundManager GetValue() => this;

        public void AddSoundType(SoundTypes type, bool isLoop)
        {
            if (!_sources.ContainsKey(type))
            {
                AudioSource sound = Instantiate(new GameObject($"source {type}"), transform).AddComponent<AudioSource>();
                _sources.Add(type, sound);
            }
            _sources[type].loop = isLoop;
        }

        public void PlaySound(SoundTypes type, bool isLoop, AudioClip clip)
        {
            if (!_sources.ContainsKey(type))
                AddSoundType(type, isLoop);

            AudioSource source = _sources[type];
            source.clip = clip;
            source.Play();
        }

        private void SetVolume()
        {
            foreach (var item in _sources)
                item.Value.volume = _volumeSounds;
        }

        public float SoundVolume { get => _volumeSounds; set { _volumeSounds = value; SetVolume(); } }
    }

    public enum SoundTypes
    {
        UIClick = 0,
        Music = 1,
        Play = 2
    }
}