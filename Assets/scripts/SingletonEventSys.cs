using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonEventSys : MonoBehaviour
{
    public static SingletonEventSys instance;

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

}
