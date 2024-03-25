using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class StorePanel  : BasePanel
{
    private static readonly string path = "StorePanel";

    public StorePanel() : base(new UIType(path)) { }

    public BagItem currentItem;

    public Text FooterText;
    
    private Button buyBtn;



    public override void OnEnter()
    {
        GameObject storePanel = GameObject.Find("Canvas/StorePanel");

        FooterText = storePanel.transform.Find("Commodity/Footer/Text").GetComponent<Text>();
        buyBtn = storePanel.transform.Find("Buy").GetComponent<Button>();

        UITool.GetOrAddComponentInChildren<Button>("BtnExit").onClick.AddListener(()=>
        {
            AudioManager.Instance.PlayButtonAudio();
            Pop();
        });
        
        buyBtn.onClick.AddListener(() =>
        {
            MainSceneManager.Instance.PlayerConctroller.characterStats.Money =
                MainSceneManager.Instance.PlayerConctroller.characterStats.Money - currentItem.price;
            InventoryManager.Instance.AddNewItem(currentItem);
        });
    }
}
