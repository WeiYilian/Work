using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task
{
    public int Index { get; set; }
    
    public string TaskName { get; set; }

    public string TaskDemand { get; set; }
    
    public int TaskReward { get; set; }

    public int TaskCompletion = 0;
    
    public int TaskAim { get; set; }

    public bool IsAcceptTask;

    public bool IsFinish;
}
