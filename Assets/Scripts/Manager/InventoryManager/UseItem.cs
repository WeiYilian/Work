using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class UseItem : MonoBehaviour,IPoolable
{
    private static UseItem _instance;

    private void Awake()
    {
        if (_instance == null)
                 _instance = this;
        else if (_instance != this)
        {
            Destroy(_instance.gameObject);
            _instance = this;
        }
    }

    public BagItem Useitem { get; set; }
    
    private void Start()
    {
        
        
        transform.Find("UsePanel/Use").GetComponent<Button>().onClick.AddListener(() =>
        {
            switch (Useitem.itemName)
            {
                case "RestoreDrug":
                    PlayerConctroller.Instance.characterStats.CurrentHealth = Mathf.Min(PlayerConctroller.Instance.characterStats.CurrentHealth+50,PlayerConctroller.Instance.characterStats.MaxHealth);
                    if (Useitem.itemHeld == 1)
                        PlayerConctroller.Instance.myBag.itemList.Remove(Useitem);
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
            PlayerConctroller.Instance.myBag.itemList.Remove(Useitem);
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
