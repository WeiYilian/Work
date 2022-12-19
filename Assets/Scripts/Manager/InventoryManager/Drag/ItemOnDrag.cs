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

    private void Start()
    {
        rightClick.AddListener(new UnityAction<PointerEventData>(ButtonRightClick));
    }

    //开始拖动时的事件
    public void OnBeginDrag(PointerEventData eventData)
    {
        //获取原来的格子位置
        originalParent = transform.parent;  
        //将所要移动的物体变成到父物体的父物体的子物体
        transform.SetParent(transform.parent.parent);
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
            //没有物品的话，是返回的Slot
            transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);
            transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            return;
        }
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
    }
}
