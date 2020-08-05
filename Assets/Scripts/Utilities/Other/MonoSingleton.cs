using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError(typeof(T).ToString() + " is NULL");
            }

            return _instance;
        }
    }

    public virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;

            Init();
        }
        else
        {
            Debug.LogError(typeof(T).ToString() + " is double instancies!");

            Destroy(this.gameObject);
        }
    }

    public virtual void Init() { }
}
