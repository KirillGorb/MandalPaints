using UnityEngine;

namespace Sound
{
    public class SoundPlayOnAction : MonoBehaviour
    {
        [SerializeField] private SoundModel model;
        [SerializeField] private SoundTypes type;
        [SerializeField] private bool isLoop = false;

        public void ClickSound() =>
            SoundManager.Instancy.PlaySound(type, isLoop, model[type]);
    }
}