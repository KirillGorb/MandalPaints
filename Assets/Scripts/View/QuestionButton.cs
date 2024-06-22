using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class QuestionButton : MonoBehaviour
    {
        [SerializeField] private TMP_Text viewResponse;
        [SerializeField] private Image viewPlane;
        [SerializeField] private Button viewCheck;

        [SerializeField] private ConfigResponse config;

        private WaitForSeconds _seconds;
        private bool _isRightAnswer;
        private Action<bool> _check;

        private void Start()
        {
            _seconds = new(config.TimeCheck);
            viewCheck.onClick.AddListener(Check);
        }

        private void OnEnable()
        {
            SetInit();
        }

        private void OnDestroy()
        {
            viewCheck.onClick.AddListener(Check);
        }

        public void SetActive(bool isActive) => gameObject.SetActive(isActive);

        public void SetData(string text, bool isRight, Action<bool> check)
        {
            viewResponse.text = text;
            _isRightAnswer = isRight;
            _check = check;
        }

        public void ActiveIsRight()
        {
            if (_isRightAnswer)
                ColorActive();
        }

        private void Check() => StartCoroutine(TimeColor());

        private IEnumerator TimeColor()
        {
            ColorActive();
            yield return _seconds;
            SetInit();
            _check?.Invoke(_isRightAnswer);
        }

        private void SetInit()
        {
            var s = config.GetCheckPlaneTextColor();
            viewPlane.color = s.Item1;
            viewResponse.color = s.Item2;
        }

        private void ColorActive()
        {
            viewPlane.color = config.IsCheckPlane(_isRightAnswer);
            viewResponse.color = config.IsCheckText(_isRightAnswer);
        }
    }
}