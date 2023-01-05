using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IAssetFactory
{

    GameObject LoadGameObject(string name);

    AudioClip loadAudioClip(string name); //音乐;

    Sprite LoadSprite(string name);//图片
}
