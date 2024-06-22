using TMPro;
using UnityEngine;

namespace View
{
    public class LidsItem : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;

        public void Set(string msg) => text.text = msg;
    }
}