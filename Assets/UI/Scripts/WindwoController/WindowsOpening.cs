using UnityEngine;

namespace Windows
{
    public class WindowsOpening : MonoBehaviour
    {
        [SerializeField] private string windowID;
        [SerializeField] private Transform parent;

        public void AddWindow() =>
            WindowsLooping.Instancy.AddWindow(windowID, parent);
        public void AddWindowToCanvas() =>
            WindowsLooping.Instancy.AddWindow(windowID, FindObjectOfType<Canvas>().transform);
        public void ActiveWindow() =>
            WindowsLooping.Instancy.ActiveWindow(windowID);

        private void OnDestroy()
        {
            WindowsLooping.Instancy.Clear();
        }
    }
}