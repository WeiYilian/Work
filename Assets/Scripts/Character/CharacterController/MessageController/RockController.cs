using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RockController : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.F))
        {
            if(!TaskManager.Instance.Tasks[0].IsAcceptTask)
                PanelManager.Instance.Push(new ConfirmPanel("请确认是否接受新手任务\n“击杀十位邪恶法师”"));
        }
    }
}
