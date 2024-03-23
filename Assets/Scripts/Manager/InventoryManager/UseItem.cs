using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class UseItem : MonoBehaviour,IPoolable
{
    private static UseItem _instance;

    private PlayerConctroller playerConctroller;

    private void Awake()
    {
        if (_instance == null)
                 _instance = this;
        else if (_instance != this)
        {
            Destroy(_instance.gameObject);
            _instance = this;
        }

        playerConctroller = MainSceneManager.Instance.PlayerConctroller;
    }

    public BagItem Useitem { get; set; }
    
    private void Start()
    {
        
        
        transform.Find("UsePanel/Use").GetComponent<Button>().onClick.AddListener(() =>
        {
            switch (Useitem.itemName)
            {
                case "RestoreDrug":
                    playerConctroller.characterStats.CurrentHealth = Mathf.Min(playerConctroller.characterStats.CurrentHealth+50,playerConctroller.characterStats.MaxHealth);
                    if (Useitem.itemHeld == 1)
                        playerConctroller.myBag.itemList.Remove(Useitem);
                    else
                        Useitem.itemHeld--;
                    InventoryManager.Instance.RefreshBagItem();
                    ObjectPool.Instance.Remove("UsePanel",gameObject);
                    break;
            }
        });
        
        transform.Find("UsePanel/Discard").GetComponent<Button>().onClick.AddListener(() =>
        {
            Useitem.itemHeld = 1;
            playerConctroller.myBag.itemList.Remove(Useitem);
            ObjectPool.Instance.Remove("UsePanel",gameObject);
        });
        
    }

    public void Dispose()
    {
        gameObject.SetActive(false);
    }

    public void Init()
    {
        gameObject.SetActive(true);
    }
}
