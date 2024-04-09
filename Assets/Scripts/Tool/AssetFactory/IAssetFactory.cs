using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IAssetFactory
{
    GameObject LoadGameObject(string resName, string filePath);

    AudioClip loadAudioClip(string resName, string filePath); //音乐;

    Sprite LoadSprite(string resName, string filePath);//图片

    RuntimeAnimatorController LoadAnimator(string resName, string filePath);
}
