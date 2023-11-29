using UnityEngine;
using UnityEngine.UI;

namespace Sound
{
    public class SoundVolume : MonoBehaviour
    {
        [SerializeField] private Slider sliderSound;

        private void Start()
        {
            var manager = SoundManager.Instancy;
            sliderSound.value = manager.SoundVolume;
            sliderSound.onValueChanged.AddListener(e => manager.SoundVolume = e);
        }
    }
}