using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance_;
    public static T instance
    {
        get
        {
            return GetInstance();
        }
    }

    private static T GetInstance()
    {
        if(instance_ != null)
        {
            return instance_;
        }
        else
        {
            GameObject go = new GameObject();
            go.AddComponent<T>();
            go.name = typeof(T).ToString();
            return go.GetComponent<T>();
        }
    }

    private void Awake()
    {
        instance_ = gameObject.GetComponent<T>();
    }

    private void Reset()
    {
        gameObject.name = typeof(T).ToString();
    }
}
