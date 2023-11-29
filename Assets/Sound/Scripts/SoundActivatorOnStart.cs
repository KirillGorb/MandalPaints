using UnityEngine;

namespace Sound
{
    public class SoundActivatorOnStart : MonoBehaviour
    {
        [SerializeField] private SoundPlayOnAction sound;
        private void Start()
        {
            sound.ClickSound();
        }
    }
}