using System.Collections.Generic;
using UnityEngine;
using WindowSystem.Windows;

namespace WindowSystem
{
    public class WindowAggregator : MonoBehaviour
    {
        [SerializeField] private List<WindowSetting> windows;
        [SerializeField] private WindowSetting initWindow;

        private WindowSetting _window;

        public WindowSetting LastWindow { get; private set; }

        private void Awake()
        {
            Active(initWindow);
        }

        public void Active(string windowName)
        {
            LastWindow = _window;
            foreach (var item in windows)
            {
                item.gameObject.SetActive(false);
                if (item.WindowName == windowName)
                {
                    item.gameObject.SetActive(true);
                    _window = item;
                }
            }
        }

        public void Active(WindowSetting window)
        {
            LastWindow = _window;
            bool flag = false;
            foreach (var item in windows)
            {
                item.gameObject.SetActive(false);
                if (item.WindowName == window.WindowName)
                {
                    item.gameObject.SetActive(true);
                    flag = true;
                }
            }
            if (!flag)
                windows.Add(Instantiate(window));

            _window = window;
        }
    }
}