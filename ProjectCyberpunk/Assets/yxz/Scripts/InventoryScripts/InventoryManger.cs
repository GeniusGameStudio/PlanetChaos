using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.EventSystems;
using UnityEngine.EventSystems;

public class InventoryManger : MonoBehaviour
{
    static InventoryManger instance;
    
    public Inventory myBag_1, myBag_2 ;
    private Inventory currentBag;
    public GameObject slotGrid;     //背包格         
    public GameObject emptySlot;    //空格
    public Text itemInfromation;    //物品详情

    public List<GameObject> slots = new List<GameObject>();

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        instance = this;
        Reseting();
    }

    //每次打开背包时更新背包信息
    private void OnEnable()
    {
        //对当前操作队伍经行判断，打开对应队伍背包
        /*if (1队进行操作){
            currentBag = myBag_1;
        }else
        {
            currentBag = myBag_2;
        }*/
        currentBag = myBag_1;
        RefreshItem();
    }

    //更新背包文本域
    public static void UpdateItemInfo(string itemDescription)
    {
        instance.itemInfromation.text = itemDescription;
    }

    //使用装备
    public static void UseItem(int ID)
    {
        //更新装备个数
        if (instance.currentBag.itemNum[ID] > 0)
        {
            instance.currentBag.itemNum[ID]--;
            InventoryManger.RefreshItem();

        }
        
    }

    //刷新物品栏
    public static void RefreshItem()
    {
        //删除slotGrid下的子集物品
        for (int i = 0; i < instance.slotGrid.transform.childCount; i++)
        {
            if (instance.slotGrid.transform.childCount == 0)
            {
                break;
            }
            Destroy(instance.slotGrid.transform.GetChild(i).gameObject);
            instance.slots.Clear();
        }
        //重新生成对应mybag中的物品的slot
        for (int i = 0; i < instance.currentBag.itemList.Count; i++)
        {
            instance.slots.Add(Instantiate(instance.emptySlot));    
            instance.slots[i].transform.SetParent(instance.slotGrid.transform);
            instance.slots[i].GetComponent<Slot>(). SetupSlot(instance.currentBag.itemList[i]);  
        }
        
    }

    //重置背包
    public void Reseting()
    {
        for (int i = 0; i < 12; i++)
        {
            myBag_1.itemNum[i] = 0;
            myBag_2.itemNum[i] = 0;
            myBag_1.itemList[i] = null;
            myBag_2.itemList[i] = null;

        }
    }
}
