using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePanel : BasePanel
{
    private static readonly string path = "DialoguePanel";

    public DialoguePanel() : base(new UIType(path)) { }
    
    public Text nameText;//角色名字
    public Text contentText;//对话内容
    private List<Message> messageList;//列表存放对话对象
    private int index = 0;//当前对话对象的索引

    public override void OnEnter()
    {
        Init();
        Message msg = getMassage();//获取对象
        if (msg != null)
        {
            nameText.text = msg.CharacterName;//显示的角色名字
            contentText.text = msg.Content;//显示的对话内容
        }
    }

    public override void OnUpdata()
    {
        UpdateMessage();
    }


    private void Init()
    {
        nameText = UITool.GetOrAddComponentInChildren<Text>("Name");
        contentText = UITool.GetOrAddComponentInChildren<Text>("ContentText");
        //创建列表存放对话对象
        messageList = new List<Message>()
        {
            new Message{CharacterName = "森林女神", Content = "帅哥，快来玩啊"},
            new Message{CharacterName = PlayerConctroller.Instance.PlayerName, Content = "我是gay"},
            new Message{CharacterName = "森林女神", Content = "我不介意的啊"},
            new Message{CharacterName = PlayerConctroller.Instance.PlayerName, Content = "我介意"},
            new Message{CharacterName = "森林女神", Content = "好吧……那你要不要看看任务？"}
        };
    }

    private void UpdateMessage()
    {
        if (Input.GetMouseButtonDown(0) && Input.anyKey)
        {
            Message msg = getMassage();//获取对象
            if (msg != null)
            {
                nameText.text = msg.CharacterName;//显示的角色名字
                contentText.text = msg.Content;//显示的对话内容
            }
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private Message getMassage()
    {
        if (index < messageList.Count)
        {
            return messageList[index++];
        }
        else
        {
            Pop();
            Push(new TaskManagerPanel());
        }

        return null;
    }
}
