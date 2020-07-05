using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public int slotID;//空格ID 等于 物品ID
    //物品的各种信息
    public Item slotItem;
    public Image slotImage;
    public Text slotNum;
    public string slotInfo;

    public GameObject itemInSlot;
    //当物品被点击
    public void ItemOnClicked()
    {
        InventoryManger.UpdateItemInfo(slotInfo);
    }

    public void SetupSlot(Item item)
    {
        if(item == null)
        {
            itemInSlot.SetActive(false);
            return;
        }

        slotImage.sprite = item.itemImage;
        slotNum.text = item.itemHeld.ToString();
        slotInfo = item.itemInfo;
    }
}
