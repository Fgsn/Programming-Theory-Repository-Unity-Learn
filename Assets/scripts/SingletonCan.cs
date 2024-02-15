using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonCan : MonoBehaviour
{
    public static SingletonCan instance;
    public static Action OnInstaniate;

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        OnInstaniate?.Invoke();
    }
}
