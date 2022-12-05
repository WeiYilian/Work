using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveBag : MonoBehaviour,IDragHandler
{
    private RectTransform currentRect;

    private void Awake()
    {
        currentRect = GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        //移动UI面板的中心值
        currentRect.anchoredPosition += eventData.delta;
    }
}
