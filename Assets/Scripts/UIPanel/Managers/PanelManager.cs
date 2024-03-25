using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 面板管理器，用栈来存储UI
/// </summary>
public class PanelManager
{
   private static PanelManager _instance;
   //存储UI面板的栈
   private Stack<BasePanel> stackPanel;
   //UI管理器
   private UIManager uIManager;
   //方便接UI面板
   private BasePanel panel;

   public static PanelManager Instance
   {
      get
      {
         if (_instance == null)
         {
            _instance = new PanelManager();
         }
         return _instance;
      }
   }

   private PanelManager()
   {
      stackPanel = new Stack<BasePanel>();
      uIManager = new UIManager();
   }

   /// <summary>
   /// UI的入栈操作，此操作会显示一个面板
   /// </summary>
   /// <param name="nextPanel">要显示的面板</param>
   public void Push(BasePanel nextPanel)
   {
      if (stackPanel.Count > 0)
      {
         panel = stackPanel.Peek();//获取栈顶的UI面板
         panel.OnPause();//暂停面板
      }

      stackPanel.Push(nextPanel);//新UI面板入栈
      GameObject panelGo = uIManager.GetSingleUI(nextPanel.UIType);//新UI面板显示
      nextPanel.Initialize(new UITool(panelGo));//初始化UITool
      nextPanel.Initialize(this);//初始化面板管理器
      nextPanel.Initialize(uIManager);//初始化UI管理器
      nextPanel.OnEnter();
   }

   /// <summary>
   /// 执行面板的出栈操作,此操作会执行面板的OnExit方法
   /// </summary>
   public void Pop()
   {
      if (stackPanel.Count > 0)
         stackPanel.Pop().OnExit();//退出当前UI面板并将当前UI面板清除出栈
      if (stackPanel.Count > 0)
         stackPanel.Peek().OnResume();//继续栈顶端的UI面板
   }

   /// <summary>
   /// 执行所有面板的OnExit()
   /// </summary>
   public void PopAll()
   {
      while (stackPanel.Count > 0)
      {
         stackPanel.Pop().OnExit();
      }
   }

   public BasePanel CurrentPanel()
   {
      if (stackPanel.Count == 0)
         return null;
      else
         return stackPanel.Peek();
   }

   #region 游戏主界面获取
   private MainPanel mainPanel;

   public MainPanel MainPanel()
   {
      if (mainPanel == null && stackPanel.Peek().UIType.Name == "MainPanel")
         mainPanel = stackPanel.Peek() as MainPanel;
      return mainPanel;
   }
   #endregion

   #region 商店界面获取

   private StorePanel storePanel;

   public StorePanel StorePanel()
   {
      if (storePanel == null && stackPanel.Peek().UIType.Name == "StorePanel")
         storePanel = stackPanel.Peek() as StorePanel;
      return storePanel;
   }

   #endregion

   #region 强化界面获取

   private EnhancedPanel enhancedPanel;

   public EnhancedPanel EnhancedPanel()
   {
      if (enhancedPanel == null && stackPanel.Peek().UIType.Name == "EnhancedPanel")
         enhancedPanel = stackPanel.Peek() as EnhancedPanel;
      return enhancedPanel;
   }

   #endregion

   // public CharacterPanel CharacterPanel()
   // {
   //    if (stackPanel.Peek().UIType.Name == "CharacterPanel")
   //       return stackPanel.Peek() as CharacterPanel;
   //    else
   //       return null;
   // }
}
