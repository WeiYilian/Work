using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
            new Task {Index = 1,TaskName = "找到森林的宝藏", TaskDemand = "找到被黑巫师与他的骷髅士兵所掠夺的宝箱，将森林的宝藏夺回来！！",TaskReward = 0,TaskAim = 3},
            new Task {Index = 2,TaskName = "小试身手", TaskDemand = "击败一个骷髅骑士",TaskReward = 30,TaskAim = 1},
            new Task {Index = 3,TaskName = "驱魔", TaskDemand = "击败一个黑巫师",TaskReward = 50,TaskAim = 1},
            new Task {Index = 4,TaskName = "独自上路", TaskDemand = "击败五个骷髅骑士",TaskReward = 200,TaskAim = 5},
            new Task {Index = 5,TaskName = "成为驱魔人", TaskDemand = "击败三个黑巫师",TaskReward = 500,TaskAim = 3}
            //在此添加后续任务
        };
    }

    public Task FindTask(int index)
    {
        foreach (var task in Tasks)
        {
            if (index == task.Index)
                return task;
        }

        return null;
    }
}
