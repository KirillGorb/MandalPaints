using System.Threading.Tasks;
using Controllers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WindowSystem.Windows
{
    public class RegistrationWindow : WindowSetting
    {
        [SerializeField] private TMP_InputField input;
        [SerializeField] private TMP_Text viewError;
        [SerializeField] private Button enter;
        [SerializeField] private ControllerDB controllerDB;

        private void Awake()
        {
            enter.interactable = false;
            input.onEndEdit.AddListener(Set);
        }

        private async void Set(string e)
        {
            enter.interactable = false;
            bool r = false;
            await Task.Run(() => r = controllerDB.SetNamePlayer(e).Result);
            enter.interactable = r;
            viewError.gameObject.SetActive(!r);
        }
    }
}