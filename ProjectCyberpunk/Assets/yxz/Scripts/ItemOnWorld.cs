using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//为地图上所有物品添加此脚本来将物品存入背包
public class ItemOnWorld : MonoBehaviour
{
    public Item thisItem;
    
    public Inventory bag_1, bag_2;
    private Inventory currentBag;

    //当玩家接触到道具
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))//Player即为玩家的Tag
        {
            //对队伍进行判断，将物品添加入对应队伍的背包
            /*if (判断为1队)
            {
                currentBag = bag_1;
            }
            else
            {
                currentBag = bag_2;
            }*/
            currentBag = bag_1;
            AddNewItem();
            Destroy(gameObject);
        }
    }

    //加入新的物品
    public void AddNewItem()
    {
        if (!currentBag.itemList.Contains(thisItem))
        {
            for (int i = 0; i < currentBag.itemList.Count; i++)
            {
                if (currentBag.itemList[i] == null)
                {
                    currentBag.itemList[i] = thisItem;
                    currentBag.itemNum[thisItem.ID]++;
                    break;
                }
            }
        }
        else
        {
            currentBag.itemNum[thisItem.ID]++;
        }

        InventoryManger.RefreshItem();
    }
}
