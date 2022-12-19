using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItem : MonoBehaviour
{
    private static UseItem _instance;

    private void Awake()
    {
        if (_instance == null)
                 _instance = this;
        else if (_instance != this)
        {
            Destroy(_instance.gameObject);
            _instance = this;
        }
    }
    
    
}
