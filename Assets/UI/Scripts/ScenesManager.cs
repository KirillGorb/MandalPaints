using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public void Exit() => Application.Quit();
    public void ActiveScen(int idScene) =>SceneManager.LoadScene(idScene);
}