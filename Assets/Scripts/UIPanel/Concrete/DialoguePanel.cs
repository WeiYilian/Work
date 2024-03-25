using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePanel : BasePanel
{
    private static readonly string path = "DialoguePanel";

    public DialoguePanel(int index) : base(new UIType(path))
    {
        msgIndex = index;
    }
    
    public Text nameText;//角色名字
    public Text contentText;//对话内容
    Message msg;
    private int msgIndex;
    
    private List<Message> messageList1 = new List<Message>()
    {
        new Message{CharacterName = "神石", Content = "勇士，我这有一些任务需要你去完成"},
        new Message{CharacterName = playerConctroller.PlayerName, Content = "什么任务？"},
        new Message{CharacterName = "神石", Content = "请看"}
    };//列表存放对话对象
    
    private List<Message> messageList2 = new List<Message>()
    {
        new Message{CharacterName = "铁匠", Content = "勇士，我可以帮你强化武器"},
        new Message{CharacterName = playerConctroller.PlayerName, Content = "看看"},
        new Message{CharacterName = "铁匠", Content = "请看"}
    };//列表存放对话对象

    private List<Message> messageList3 = new List<Message>()
    {
        new Message{CharacterName = "商人", Content = "我可以提供一些物资，前提是你有足够的金币"},
        new Message{CharacterName = playerConctroller.PlayerName, Content = "我可以看看你的商品吗"},
        new Message{CharacterName = "商人", Content = "请看"}
    };//列表存放对话对象

    private List<Message> messagesList;
    
    private int index = 0;//当前对话对象的索引

    public override void OnEnter()
    {
        Init();
        
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
        switch (msgIndex)
        {
            case 1:
                messagesList = messageList1;
                break;
            case 2:
                messagesList = messageList2;
                break;
            case 3:
                messagesList = messageList3;
                break;
        }
        msg = getMassage(messagesList);
    }

    private void UpdateMessage()
    {
        if (Input.GetMouseButtonDown(0) && Input.anyKey)
        {
            msg = getMassage(messagesList);
            if (msg != null)
            {
                nameText.text = msg.CharacterName;//显示的角色名字
                contentText.text = msg.Content;//显示的对话内容
            }
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private Message getMassage(List<Message> messageList)
    {
        if (index < messageList.Count)
        {
            return messageList[index++];
        }
        else
        {
            Pop();
            switch (msgIndex)
            {
                case 1:
                    Push(new TaskManagerPanel());
                    break;
                case 2:
                    Debug.Log("武器强化");
                    Push(new EnhancedPanel());
                    break;
                case 3:
                    Debug.Log("购买商品");
                    Push(new StorePanel());
                    break;
            }
            
        }

        return null;
    }
}
