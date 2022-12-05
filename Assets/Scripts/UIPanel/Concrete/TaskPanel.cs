using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskPanel : BasePanel
{
    private static readonly string path = "UI/Panel/TaskPanel";
    
    public TaskPanel():base(new UIType(path)){}

    private List<Task> tasks = new List<Task>();
    
    public override void OnEnter()
    {
        Init();

        UITool.GetOrAddComponentInChildren<Button>("BtnExit").onClick.AddListener(Pop);
        
        UITool.GetOrAddComponentInChildren<Button>("Submit").onClick.AddListener(() =>
        {
            if (tasks[0].TaskCompletion == tasks[0].TaskAim)
            {
                PanelManager.Instance.Push(new OverPanel("任务成功"));
                Time.timeScale = 0;
            }
            else
            {
                Push(new NoticePanel("任务未完成，请多加油！"));
            }
        });
    }

    public override void OnUpdata()
    {
        if (Input.GetKeyDown(KeyCode.P))
            Pop();
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void Init()
    {
        Transform Login = GameObject.Find("Canvas").transform.Find("TaskPanel/Login");
        foreach (var task in TaskManager.Instance.Tasks)
        {
            if (task.IsAcceptTask)
            {
                Debug.Log(task.TaskCompletion + task.TaskName);
                tasks.Add(task);
            }
                
        }
        if (tasks != null)
        {
            Login.Find("Image/TaskName").GetComponent<Text>().text = tasks[0].TaskName;
            Login.Find("Image/TaskDemand").GetComponent<Text>().text = tasks[0].TaskDemand;
            Login.Find("Image/TaskCompletion").GetComponent<Text>().text = $"目前任务完成度：{tasks[0].TaskCompletion}/{tasks[0].TaskAim}";
        }
    }
}
