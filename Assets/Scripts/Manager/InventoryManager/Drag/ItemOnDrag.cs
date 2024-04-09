using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class ItemOnDrag : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler, IPointerClickHandler
{
    public static Action<Image> quickUseDrug;
    
    private Transform originalParent;
    
    public UnityEvent<PointerEventData> rightClick;

    private Slot startSlot;
    private Slot endSlot;
    
    EquipController equipController;
    List<BagItem> itemList;
    
    private void Start()
    {
        rightClick.AddListener(ButtonRightClick);
        equipController = MainSceneManager.Instance.PlayerConctroller.EquipController;
        itemList = MainSceneManager.Instance.PlayerConctroller.myBag.itemList;
    }
    
    //开始拖动时的事件
    public void OnBeginDrag(PointerEventData eventData)
    {
        //获取原来的格子位置
        originalParent = transform.parent;  
        //获取原来的物品信息
        startSlot = originalParent.GetComponent<Slot>();
        //将所要移动的物体变成到父物体的父物体的子物体
        transform.SetParent(transform.parent.parent.parent.parent.parent);
        //物体的位置等于鼠标的位置
        transform.position = eventData.position;
        //关闭BlocksRaycasts功能，这样发射的射线可以返回拖动的物体下面一层的东西
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    //拖动过程中的事件
    public void OnDrag(PointerEventData eventData)
    {
        //物体的位置等于鼠标的位置
        transform.position = eventData.position;
    }

    //拖动结束时的事件
    public void OnEndDrag(PointerEventData eventData)
    {
        //如果返回的信息名字为Item Image，表示有物品，需要交换
        if (eventData.pointerCurrentRaycast.gameObject.transform.name == "Item Image")
        {
            endSlot = eventData.pointerCurrentRaycast.gameObject.transform.parent.GetComponent<Slot>();
            if (!DetermineType())
            {
                Reposition();
                return;
            }
            
            var parent = eventData.pointerCurrentRaycast.gameObject.transform.parent;
            //由于指向物体是Item Image，即Item Image的父物体的父物体才是Slot,故要将拖拽的物体的父物体设成Item Image所在的Slot
            transform.SetParent(parent.parent);
            //将拖拽的物体的位置等于所要拖到的地方的位置
            transform.position = parent.parent.position;
            //将被交换的物体位置移到拖拽的物体位置
            parent.position = originalParent.position;
            //设置被交换的物体的父物体为拖拽的物体的父物体
            parent.SetParent(originalParent);
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            return;
        }
        //放到空格子
        if(eventData.pointerCurrentRaycast.gameObject.transform.name == "Slot(Clone)")
        {
            endSlot = eventData.pointerCurrentRaycast.gameObject.transform.GetComponent<Slot>();
            if (!DetermineType())
            {
                Reposition();
                return;
            }

            Debug.Log("装备");
            //没有物品的话，是返回的Slot
            transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);
            transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            return;
        }

        Reposition();
    }


    public void Reposition()
    {
        transform.SetParent(originalParent);
        transform.position = originalParent.position;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        //如果是右键点击button就触发事件
        if (eventData.button == PointerEventData.InputButton.Right)
            rightClick.Invoke(eventData);
    }
    
    //点击右键触发的事件
    private void ButtonRightClick(PointerEventData eventData)
    {
        GameObject go = ObjectPool.Instance.Get("UsePanel", transform.parent.parent.parent);
        go.transform.position = eventData.position;
        go.GetComponent<UseItem>().Useitem = transform.parent.GetComponent<Slot>().slotItem;
    }

    //判断交换物品的类型
    public bool DetermineType()
    {
        if (endSlot.WeaponBox && startSlot.slotItem.equipType == 1)
        {
            ReplaceWay1();
            return true;
        }

        if (endSlot.ArmorBox && startSlot.slotItem.equipType == 2)
        {
            ReplaceWay1();
            return true;
        }

        if (endSlot.QuickItemsBox && startSlot.slotItem.equipType == 3)
        {
            ReplaceWay1();
            return true;
        }
        
        if (startSlot.WeaponBox)
        {
            ReplaceWay2(1);
            return true;
        }

        if (startSlot.ArmorBox)
        {
            ReplaceWay2(2);
            return true;
        }

        if (startSlot.QuickItemsBox)
        {
            ReplaceWay2(3);
            return true;
        }
        
        if (!endSlot.WeaponBox && !endSlot.ArmorBox && !endSlot.QuickItemsBox) return true;
        
        Debug.Log("装备失败");
        return false;
    }

    public void ReplaceWay1()
    {
        if(endSlot.slotItem) InventoryManager.Instance.AddNewItem(endSlot.slotItem);
        equipController.ReplaceWeapon(startSlot.slotItem);
        itemList.Remove(startSlot.slotItem);
        itemList.Add(null);
    }

    public void ReplaceWay2(int index)
    {
        if (!endSlot.slotItem) equipController.ReplaceWeapon(null);
            
        if (endSlot.slotItem && endSlot.slotItem.equipType == index)
        {
            equipController.ReplaceWeapon(endSlot.slotItem);
            itemList.Remove(endSlot.slotItem);
            itemList.Add(null);
        }
        InventoryManager.Instance.AddNewItem(startSlot.slotItem);
    }
}
