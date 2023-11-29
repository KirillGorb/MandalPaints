using System.Collections.Generic;
using UnityEngine;

namespace Sound
{
    [CreateAssetMenu()]
    public class SoundModel : ScriptableObject
    {
        [SerializeField] private List<SoundsItem> sounds;

        public AudioClip this[SoundTypes type]
        {
            get
            {
                AudioClip[] clips = sounds[(int)type].Clips;
                return clips[Random.Range(0, clips.Length)];
            }
        }
    }

    [System.Serializable]
    public struct SoundsItem
    {
        public SoundTypes TypeSound;
        public AudioClip[] Clips;
    }
}