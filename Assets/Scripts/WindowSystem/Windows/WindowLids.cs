using System;
using System.Collections.Generic;
using Controllers;
using UnityEngine;
using View;

namespace WindowSystem.Windows
{
    public class WindowLids : WindowSetting
    {
        [SerializeField] private LidsItem prefab;
        [SerializeField] private Transform parent;
        [SerializeField] private ControllerDB controllerDB;

        private readonly List<LidsItem> _items = new();

        private void OnEnable()
        {
            foreach (var item in controllerDB.GetLids())
            {
                if (item == null) return;
                var i = Instantiate(prefab, parent);
                i.Set(item);
                _items.Add(i);
            }
        }

        private void OnDisable()
        {
            foreach (var item in _items)
                Destroy(item.gameObject);
            _items.Clear();
        }
    }
}