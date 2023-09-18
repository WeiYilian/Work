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
  

   public UIType(string name)
   {
      Name = name;
   }
}
