using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnhancedEquip : MonoBehaviour
{
    public BagItem Item;

    private PanelManager panelManager;

    private Image itemImage;

    private Text itemText;

    private void Awake()
    {
        panelManager = PanelManager.Instance;

        itemImage = transform.GetChild(0).GetComponent<Image>();
        itemText = transform.GetChild(1).GetComponent<Text>();
    }

    private void Start()
    {
        if (Item)
        {
            itemImage.sprite = Item.itemImage;
            if(Item.equipLevel > 0)
                itemText.text = "+" + Item.equipLevel;
        }
        else
        {
            Color color = itemImage.color;
            color.a = 0;
            itemImage.color = color;
        }
        
        GetComponent<Button>().onClick.AddListener(() =>
        {
            panelManager.EnhancedPanel().CurrentEquip = Item;
            panelManager.EnhancedPanel().InitEquip();
        });
    }
}
