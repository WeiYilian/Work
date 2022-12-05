using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager
{
    private static TaskManager _instance;
    
    public static TaskManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new TaskManager();
            }
            return _instance;
        }
    }

    public List<Task> Tasks;

    public TaskManager()
    {
        Tasks = new List<Task>()
        {
            new Task {TaskName = "新手任务", TaskDemand = "请阻止黑巫师的阴谋并夺回十根法杖", TaskCompletion = 0,TaskAim = 1,IsAcceptTask = false}
            //在此添加后续任务
        };
    }
}
