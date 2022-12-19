using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RockController : MonoBehaviour
{
    private bool isFirst= true;
    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.F) && !GameLoop.Instance.isTimeOut)
        {
            isFirst = false;
            if (isFirst)
            {
                PanelManager.Instance.Push(new DialoguePanel());
                //isFirst = false;
            }
            else
                PanelManager.Instance.Push(new TaskManagerPanel());
        }
    }
}
