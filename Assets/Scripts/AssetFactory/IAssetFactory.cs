using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IAssetFactory
{
    GameObject LoadSlash(string name);//特效

    GameObject LoadAccount(string name);//用户信息面板

    GameObject LoadSlot(string name);//背包格子

    GameObject LoadEnemy(string name);//怪物

    AudioClip loadAudioClip(string name); //音乐;

    Sprite LoadSprite(string name);//图片
}
