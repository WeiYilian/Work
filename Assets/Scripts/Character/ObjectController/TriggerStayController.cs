using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerStayController : MonoBehaviour
{
    public int Index;
    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.F) && !MainSceneManager.Instance.isTimeOut)
        {
            PanelManager.Instance.Push(new DialoguePanel(Index));
            //PanelManager.Instance.Push(new TaskManagerPanel());
        }
    }
}
