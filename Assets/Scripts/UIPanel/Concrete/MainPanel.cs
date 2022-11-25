using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPanel : BasePanel
{
    private static readonly string path = "UI/Panel/MainPanel"; 

    public MainPanel() : base(new UIType(path)) { }
}
