using UnityEngine;

namespace WindowSystem.Windows
{
    public class WindowSetting : MonoBehaviour
    {
        [SerializeField] private string windowName;
        public string WindowName => windowName;
    }
}