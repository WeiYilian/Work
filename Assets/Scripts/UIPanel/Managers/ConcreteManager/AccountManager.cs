using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccountManager : MonoBehaviour
{
    public List<GameObject> Accounts = new List<GameObject>();
    
    private GameObject accountPrefab;

    private void Awake()
    {
        accountPrefab = GameFacade.Instance.LoadAccount();
    }

    private void Start()
    {
        //查询所有用户名与密码
        List<string[]> accountList = DataManager.ClickAccount();
        foreach (string[] item in accountList)
        {
            //实例化用户栏
            GameObject account = Instantiate(accountPrefab, transform, true);
            Accounts.Add(account);
            //将相对应的数据传入用户栏
            account.transform.GetChild(3).GetComponent<Text>().text = item.GetValue(0).ToString();
            account.transform.GetChild(4).GetComponent<Text>().text = $"年龄：{item.GetValue(1)}\t性别：{item.GetValue(2)}";
        }
        
        foreach (GameObject account in Accounts)
        {
            account.GetComponent<Button>().onClick.AddListener(() =>
            {
                AudioManager.Instance.PlayButtonAudio();
                string username = account.transform.GetChild(3).GetComponent<Text>().text;
                PanelManager.Instance.Push(new LoginPanel(username));
            });
            
            account.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() =>
            {
                AudioManager.Instance.PlayButtonAudio();
                //数据库中注销账号
                DataManager.DeleteData(account.transform.GetChild(3).GetComponent<Text>().text);
                Destroy(account.gameObject);
            });
        }
    }
}
