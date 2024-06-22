using UnityEngine;
using UnityEngine.SceneManagement;

namespace WindowSystem
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private string nameScene;
        
        public void ActiveScene() => SceneManager.LoadScene(nameScene);
    }
}