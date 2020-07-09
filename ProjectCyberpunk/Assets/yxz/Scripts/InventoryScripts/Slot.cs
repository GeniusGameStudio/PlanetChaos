using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.EventSystems;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerEnterHandler ,IPointerExitHandler
{

    //物品的各种信息
    public Item slotItem;
    public Image slotImage;
    public Text slotNum;
    public GameObject UseUp;
    public string slotInfo;
    public int slotID;
    public GameObject information;
   
    public Inventory Bag_1, Bag_2;
    private Inventory currentBag;

    public GameObject itemInSlot;

    public void Start()
    {
        information = GameObject.Find("Canvas_Bag/Bag/Information");
    }
   
    //当物品被点击
    public void ItemOnClicked()
    {
        InventoryManger.UseItem(slotID);
    }

    //当鼠标悬停在物品之上显示物品信息
    public void OnPointerEnter(PointerEventData eventData)
    {
        //如果道具信息不为空则会显示信息栏
        if(slotInfo != "") 
            {
            InventoryManger.UpdateItemInfo(slotInfo);
            information.SetActive(true);
        }
    }
    //当鼠标移开物品取消显示
    public void OnPointerExit(PointerEventData eventData)
    {
        information.SetActive(false);
    }

    //设置格中信息
    public void SetupSlot(Item item)
    {
        /*if (判断为1队进行操作)
            {
                currentBag = bag_1;
            }
            else
            {
                currentBag = bag_2;
            }*/
        currentBag = Bag_1;
        //如果该Slot上没有物品则不显示item
        if(item == null)
        {
            itemInSlot.SetActive(false);
            return;
        }
        isUseUp(item);
        slotImage.sprite = item.itemImage;
        slotNum.text = currentBag.itemNum[item.ID].ToString();
        slotInfo = item.itemInfo;
        slotID = item.ID;
    }

    //当被用尽时变灰白
    public void isUseUp(Item item)
    {
        UseUp.SetActive(false);
        if (currentBag.itemNum[item.ID] == 0 && item != null)
        {
            UseUp.SetActive(true);
        }
    }


}
