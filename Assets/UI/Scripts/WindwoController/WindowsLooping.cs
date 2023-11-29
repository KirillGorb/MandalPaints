using System.Collections.Generic;
using UnityEngine;

namespace Windows
{
    public class WindowsLooping : ASinglton<WindowsLooping>
    {
        [SerializeField] private WindowsModel model;

        private Dictionary<string, GameObject> _windowsLoop = new Dictionary<string, GameObject>();

        protected override WindowsLooping GetValue() => this;

        public void AddWindow(string idWindow, Transform parent)
        {
            if (!_windowsLoop.ContainsKey(idWindow))
            {
                var item = Instantiate(model[idWindow], parent);
                _windowsLoop.Add(idWindow, item);
            }
            ActiveWindow(idWindow);
        }

        public void ActiveWindow(string idWindow)
        {
            foreach (var item in _windowsLoop)
                item.Value.SetActive(false);
            _windowsLoop[idWindow].SetActive(true);
        }

        public void Clear() => _windowsLoop.Clear();
    }
}