using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                // Busca una instancia en la escena
                _instance = FindObjectOfType<T>();

                // Si no se encuentra, crea un nuevo GameObject y agrega el componente
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(T).Name + " (Singleton)");
                    _instance = singletonObject.AddComponent<T>();

                    // Marca el objeto como no destruible entre escenas
                    DontDestroyOnLoad(singletonObject);
                }
            }
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Si ya existe una instancia, destruye este objeto
            if (Application.isPlaying)
            {
                Destroy(gameObject);
            }
        }
    }
}
