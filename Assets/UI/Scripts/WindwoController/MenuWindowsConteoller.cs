using UnityEngine;

namespace Windows
{
    public class MenuWindowsConteoller : MonoBehaviour
    {
        [SerializeField] protected WindowsOpening[] windowActiveToStart;

        private void Start()
        {
            foreach (var item in windowActiveToStart)
                item.AddWindow();
        }
    }
}