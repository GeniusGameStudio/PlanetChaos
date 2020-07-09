using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveBag : MonoBehaviour, IDragHandler
{
    RectTransform currentRect;

    void Awake()
    {
        currentRect = GetComponent<RectTransform>();
    }
    //将背包的位置参数+=鼠标移动
    public void OnDrag(PointerEventData eventData)
    {
        currentRect.anchoredPosition += eventData.delta;
    }

}
