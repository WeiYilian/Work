using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class TaskManagerPanel : BasePanel
{
    private static readonly string path = "TaskManagerPanel";/*readonly：声明字段，表示声明的字段只能在声明时或同一个类的构造函数当中进行赋值*/

    public TaskManagerPanel() : base(new UIType(path)) { }

    private List<GameObject> _list = new List<GameObject>();
    
    private GameObject Task;

    private GameObject TaskPanel;

    // ReSharper disable Unity.PerformanceAnalysis
    public override void OnEnter()
    {
        Init();
        
        UITool.GetOrAddComponentInChildren<Button>("BtnExit").onClick.AddListener(Pop);

        foreach (var mission in _list)
        {
            mission.transform.Find("BtnAccept").GetComponent<Button>().onClick.AddListener(() =>
            {
                foreach (var task in TaskManager.Instance.Tasks)
                {
                    if (mission.transform.Find("TaskName").GetComponentInChildren<Text>().text == task.TaskName)
                    {
                        Object.Destroy(mission);
                        task.IsAcceptTask = true;
                        PanelManager.Push(new NoticePanel("任务接受成功！"));
                    }
                }
            });
        }
    }

    public void Init()
    {
        Task = GameFacade.Instance.LoadGameObject("Task");
        TaskPanel = GameObject.Find("TaskManagerPanel").transform.Find("Contents/Scroll View/Viewport/Content").gameObject;

        foreach (var task in TaskManager.Instance.Tasks)
        {
            if (!task.IsAcceptTask && !task.IsFinish)
            {
                if((task.Index == 4 && !TaskManager.Instance.Tasks[1].IsFinish) || (task.Index == 5 && !TaskManager.Instance.Tasks[2].IsFinish))
                    return;
                GameObject obj = GameObject.Instantiate(Task,TaskPanel.transform,true);
                obj.transform.Find("TaskName").GetComponent<Text>().text = task.TaskName;
                _list.Add(obj);
            }
        }
    }
}
