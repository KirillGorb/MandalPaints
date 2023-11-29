using UnityEngine;

public abstract class ASinglton<T> : MonoBehaviour where T : MonoBehaviour
{

    public static T Instancy;

    private void Awake()
    {
        if (Instancy == null)
        {
            Instancy = GetValue();
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    protected abstract T GetValue();
}