using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemOnDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform originalParent;
    public Inventory myBag;
    private int currentItemID;//当前物品ID

    //开始拖拽
    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        currentItemID = originalParent.GetComponent<Slot>().slotID;
        transform.SetParent(transform.parent.parent);
        transform.position = eventData.position;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    //拖拽中
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        //Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
    }

    //拖拽结束
    public void OnEndDrag(PointerEventData eventData)
    {
        //移动到其他地方
        transform.SetParent(originalParent);
        transform.position = originalParent.position;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        //如果将道具移动到另一个道具上
        if (eventData.pointerCurrentRaycast.gameObject.name == "Image_item")
        {
            //itemList物品位置调换
            var temp = myBag.itemList[currentItemID];
            myBag.itemList[currentItemID] = myBag.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID];
            myBag.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID] = temp;
            
            transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent);
            transform.position = eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.position;
            eventData.pointerCurrentRaycast.gameObject.transform.parent.position = originalParent.position;
            eventData.pointerCurrentRaycast.gameObject.transform.parent.SetParent(originalParent);
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            return;
        }
        //如果移动到空格上
        if(eventData.pointerCurrentRaycast.gameObject.name == "Slot(Clone)")
        {
            //itemList中物品位置调换
            myBag.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID] = myBag.itemList[currentItemID];
            if (eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID != currentItemID)
            {
                myBag.itemList[currentItemID] = null;
            }

            transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);
            transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            return;
        }
    }
}
