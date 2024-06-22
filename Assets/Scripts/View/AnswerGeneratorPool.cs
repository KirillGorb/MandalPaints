using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Controllers;

namespace View
{
    public class AnswerGeneratorPool : MonoBehaviour
    {
        [SerializeField, Space(5)] private int testView;


        [SerializeField] private TMP_Text textAnswer;

        [SerializeField] private QuestionButton prefab;
        [SerializeField] private Transform parent;

        private readonly List<QuestionButton> _pool = new();

        private int _capCount;

#if UNITY_EDITOR
        [ContextMenu("reset cap")]
        private void Reset()
        {
            testView = 0;
            _capCount = 0;

            _pool.Clear();
        }

        private void OnValidate()
        {
            Spawn(testView);
        }
#endif

        public void Spawn(int count)
        {
            if (count == _capCount)
            {
                foreach (var item in _pool)
                    item.SetActive(true);
            }
            else if (count > _capCount)
            {
                foreach (var item in _pool)
                    item.SetActive(true);
                var dop = count - _capCount;
                for (int i = 0; i < dop; i++)
                    _pool.Add(Instantiate(prefab, parent));
            }
            else
            {
                foreach (var item in _pool)
                    item.SetActive(false);
                for (int i = 0; i < count; i++)
                    _pool[i].SetActive(true);
            }

            _capCount = _pool.Count;
        }

        public void SetText(string text) => textAnswer.text = text;

        public void SetData(IReadOnlyCollection<Resource> resources, Action actionTrue, Action actionNext)
        {
            int i = 0;
            foreach (var item in resources)
                _pool[i++].SetData(item.Text, item.IsRight, e =>
                {
                    if (e)
                        actionTrue();
                    else
                        foreach (var response in _pool)
                            response.ActiveIsRight();
                    actionNext();
                });
        }
    }
}