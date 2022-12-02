using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    private void Start()
    {
        PanelManager.Instance.Push(new MainPanel());
    }
}
