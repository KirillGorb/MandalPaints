using Controllers;
using TMPro;
using UnityEngine;

namespace WindowSystem.Windows
{
    public class ResWindow : WindowSetting
    {
        [SerializeField] private ControllerGame controllerGame;
        [SerializeField] private ControllerDB controllerDB;
        [SerializeField] private TMP_Text textRes;

        private void Awake()
        {
            controllerGame.Res += Res;
        }

        private void OnDestroy()
        {
            controllerGame.Res -= Res;
        }

        private void Res(int res)
        {
            textRes.text = res.ToString();
            controllerDB.SetProgress(res);
        }
    }
}