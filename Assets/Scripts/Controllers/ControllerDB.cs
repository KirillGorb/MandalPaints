using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Database;
using UnityEngine;

namespace Controllers
{
    public class ControllerDB : MonoBehaviour
    {
        private const string KeyGame = "game";
        private readonly string[] _keyLevel = { "light", "medium", "hard" };

        private const string KeyDates = "datas";
        private const string KeyData = "d";
        private const string KeyResponse = "response";

        private const string KeyQuestions = "questions";

        private const string KeyQValue = "value";
        private const string KeyQBool = "bool";

        private const string KeyTimes = "times";
        private const string KeyTime = "t";

        private const string KeyLids = "players";

        private const int StockId = 2;

        private readonly Dictionary<string, object>[] _dataContainers = new Dictionary<string, object>[3];
        private Dictionary<string, object> _timerContainer;
        private Dictionary<string, object> _lidsContainer;

        private DatabaseReference _reference;
        private int _id = 1;

        private string _namePlayer;

        public async Task<bool> SetNamePlayer(string value)
        {
            bool res = false;
            await _reference.GetValueAsync().ContinueWith(task =>
            {
                if (!task.Result.Child(KeyGame).Child(KeyLids).HasChild(value))
                {
                    res = true;
                    _namePlayer = value;
                }
            });
            return res;
        }

        public string[] GetLids(int max = 10)
        {
            string[] str = new string[max];
            int i = 0;
            foreach (var item in _lidsContainer)
            {
                if (i >= max) break;
                str[i++] = item.Key + ": " + item.Value;
            }

            return str;
        }

        public float[] GetTime(int levelId)
        {
            float[] rs = new float[3];
            for (int i = 0; i < rs.Length; i++)
                rs[i] = (long)SetDataType<object>(_timerContainer[_keyLevel[levelId]])[$"{KeyTime}{i + 1}"];
            return rs;
        }

        public (string, List<Resource>) SetData(int levelId)
        {
            var resp = "";
            List<Resource> rs = new();

            if (_dataContainers[levelId].Count < _id + 2)
                DopData(levelId);

            if (!_dataContainers[levelId].ContainsKey($"d{_id}"))
                return (resp, rs);

            foreach (var item in SetDataType<object>(_dataContainers[levelId][$"{KeyData}{_id}"]))
            {
                if (item.Key == KeyResponse)
                {
                    resp = item.Value.ToString();
                }
                else if (item.Key == KeyQuestions)
                {
                    foreach (var itemsQ in SetDataType<object>(item.Value))
                    {
                        var i = SetDataType<object>(itemsQ.Value);
                        var v = (string)i[KeyQValue];
                        var b = (bool)i[KeyQBool];
                        rs.Add(new Resource
                        {
                            Text = v,
                            IsRight = b,
                        });
                    }
                }
            }

            _id++;
            return (resp, rs);
        }

        private async void Awake()
        {
            _reference = FirebaseDatabase.DefaultInstance.RootReference;

            await _reference.GetValueAsync().ContinueWith(task =>
                _lidsContainer = SetDataType<object>(task.Result.Child(KeyGame).Child(KeyLids).Value));

            await _reference.GetValueAsync()
                .ContinueWith(task =>
                {
                    _timerContainer = SetDataType<object>(task.Result.Child(KeyGame).Child(KeyTimes).Value);

                    for (int i = 0; i < 3; i++)
                    {
                        _dataContainers[i] = new();
                        Check(i, task);
                    }
                });
        }

        public async void SetProgress(int progress)
        {
            await _reference.GetValueAsync().ContinueWith(task =>
            {
                var s = SetDataType<object>(task.Result.Child(KeyGame).Child(KeyLids).Value);
                s.Add(_namePlayer, progress);
                _reference.Child(KeyGame).Child(KeyLids).SetValueAsync(s);
            });
        }

        private async void DopData(int id) =>
            await _reference.GetValueAsync().ContinueWith(task => Check(id, task));

        private void Check(int i, Task<DataSnapshot> task)
        {
            var s = _dataContainers[i].Count;
            for (int j = 1; j < s - 3; j++)
            {
                var k = $"{KeyData}{j}";
                _dataContainers[i].Remove(k);
            }

            for (int j = s + 1; j < s + StockId; j++)
            {
                var k = $"{KeyData}{j}";
                var ss = task.Result.Child(KeyGame).Child(KeyDates).Child(_keyLevel[i]);
                if (!ss.HasChild(k)) return;
                if (_dataContainers[i].ContainsKey(k)) continue;
                try
                {
                    _dataContainers[i].Add(k, SetDataType<object>(ss.Value)[k]);
                }
                catch
                {
                }
            }
        }

        private static Dictionary<string, T> SetDataType<T>(object obj) => (Dictionary<string, T>)obj;
    }
}