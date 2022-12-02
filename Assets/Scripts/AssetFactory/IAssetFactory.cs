using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAssetFactory
{
    GameObject LoadSlash(string name);//特效

    GameObject LoadAccount(string name);//用户信息面板

    GameObject LoadSlot(String name);//背包格子
}
