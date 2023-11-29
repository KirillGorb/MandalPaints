using System.Collections.Generic;
using UnityEngine;

namespace Windows
{
    [CreateAssetMenu()]
    public class WindowsModel : ScriptableObject
    {
        [SerializeField] private List<Window> windows;

        private Dictionary<string, GameObject> _windowDictionary;

        public GameObject this[string ID]
        {
            get
            {
                if (_windowDictionary == null)
                {
                    _windowDictionary = new Dictionary<string, GameObject>();
                    foreach (var item in windows)
                    {
                        var window = item.ItemWindow;
                        _windowDictionary.Add(window.Item1, window.Item2);
                    }
                }
                return _windowDictionary[ID];
            }
        }
    }
}