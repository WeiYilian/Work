using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Merchandise : MonoBehaviour
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
      if (Item) itemImage.sprite = Item.itemImage;
      else
      {
         Color color = itemImage.color;
         color.a = 0;
         itemImage.color = color;
      }

      GetComponent<Button>().onClick.AddListener(() =>
      {
         panelManager.StorePanel().FooterText.text = Item.itemInfo + "\n需要" + Item.price + "金币";
         panelManager.StorePanel().currentItem = Item;
      });
   }
}
