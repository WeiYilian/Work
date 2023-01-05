using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTaskPanel : BasePanel
{
    private static readonly string path = "UI/Panel/PlayerTaskPanel";
    
    public PlayerTaskPanel():base(new UIType(path)){}

    private List<Task> tasks = new List<Task>();

    private Transform Login;

    private int index = 0;
    
    public override void OnEnter()
    {
        Init();

        UITool.GetOrAddComponentInChildren<Button>("BtnExit").onClick.AddListener(Pop);

        UITool.GetOrAddComponentInChildren<Button>("Right").onClick.AddListener(() =>
        {
            if (index < tasks.Count - 1)
                index++;
            else
                index = 0;
        });
        
        UITool.GetOrAddComponentInChildren<Button>("Left").onClick.AddListener(() =>
        {
            if (index > 0)
                index--;
            else
                index = tasks.Count - 1;
        });
        
        UITool.GetOrAddComponentInChildren<Button>("Submit").onClick.AddListener(() =>
        {
            if (tasks[index].TaskCompletion >= tasks[index].TaskAim)
            {
                if (tasks[index].Index == 1)
                {
                    AudioManager.Instance.PlayAudio(5,"胜利音效");
                    PanelManager.Instance.Push(new OverPanel("游戏胜利"));
                }
                else
                {
                    PlayerConctroller.Instance.characterStats.characterData.UpdateExp(tasks[index].TaskReward);
                    if (TaskManager.Instance.FindTask(tasks[index].Index) != null)
                        TaskManager.Instance.FindTask(tasks[index].Index).IsFinish = true;
                    //TaskManager.Instance.Tasks.Remove(tasks[index]);
                    tasks.Remove(tasks[index]);

                    Pop();
                }
            }
            else
            {
                Push(new NoticePanel("任务未完成，请多加油！"));
            }
        });
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public override void OnUpdata()
    {
        if (Input.GetKeyDown(KeyCode.P))
            Pop();
        
        if(tasks.Count != 0)
        {
            Login.Find("Image/TaskName").GetComponent<Text>().text = tasks[index].TaskName;
            Login.Find("Image/TaskDemand").GetComponent<Text>().text = tasks[index].TaskDemand;
            if(tasks[index].Index == 1)
                Login.Find("Image/TaskReward").GetComponent<Text>().text = "获得游戏胜利";
            else
                Login.Find("Image/TaskReward").GetComponent<Text>().text = "奖励 " + tasks[index].TaskReward + " 经验值";
            Login.Find("Image/TaskCompletion").GetComponent<Text>().text = $"目前任务完成度：{tasks[index].TaskCompletion}/{tasks[index].TaskAim}";
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void Init()
    {
        Login = GameObject.Find("Canvas").transform.Find("PlayerTaskPanel/Login");
        foreach (var task in TaskManager.Instance.Tasks)
        {
            if (task.IsAcceptTask && !task.IsFinish)
            {
                tasks.Add(task);
            }

        }
    }
}
