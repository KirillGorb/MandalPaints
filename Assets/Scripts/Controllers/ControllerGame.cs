using System;
using UnityEngine;
using View;
using WindowSystem;

namespace Controllers
{
    public class ControllerGame : MonoBehaviour
    {
        [SerializeField] private AnswerGeneratorPool generatorPool;
        [SerializeField] private ControllerDB controllerDB;
        [SerializeField] private WindowAggregator windowAggregator;
        [SerializeField] private string windowNameRes;

        [SerializeField] private float timeUse;

        private float[] _times;
        private int _rep;

        public event Action<int> Res;

        public int IdLevel { get; set; }

        private void OnEnable()
        {
            ReadDBOnRender();
            _times = controllerDB.GetTime(IdLevel);
        }

        private void ReadDBOnRender()
        {
            var res = controllerDB.SetData(IdLevel);

            if (res.Item1 == "")
            {
                windowAggregator.Active(windowNameRes);
                Res?.Invoke(_rep);
                return;
            }

            generatorPool.SetText(res.Item1);
            generatorPool.Spawn(res.Item2.Count);
            generatorPool.SetData(res.Item2, Check, ReadDBOnRender);
        }

        private void Update()
        {
            timeUse += Time.deltaTime;

            if (_times[2] < timeUse)
                Res?.Invoke(_rep);
        }

        private void Check()
        {
            for (int i = 0; i < _times.Length; i++)
                if (timeUse <= _times[i])
                {
                    _rep += (3 - i) * (IdLevel + 1);
                    timeUse = 0;
                    return;
                }
        }
    }
}