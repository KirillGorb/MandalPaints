using UnityEngine;

namespace Windows
{
    public class Window : MonoBehaviour
    {
        [SerializeField] private GameObject window;
        [SerializeField] private string windowID;

        public (string, GameObject) ItemWindow => (windowID, window);
    }
}