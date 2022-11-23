using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 存储单个UI的信息，包括名字和路径
/// </summary>
public class UIType
{
   //UI名字
   public string Name { get; private set; }
   //UI路径
   public string Path { get; private set; }

   public UIType(string path)
   {
      Path = path;
      Name = path.Substring(path.LastIndexOf('/') + 1);//表示name是该路径下最后一个 / 后的第一个字符开始到最后一个字符结束的字符串
   }
}
