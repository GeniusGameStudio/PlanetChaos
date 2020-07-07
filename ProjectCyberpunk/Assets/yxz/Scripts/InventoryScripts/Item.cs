using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/New Item")]
public class Item : ScriptableObject
{
    //存放道具的各种属性
    public int ID;
    public string itemName;
    public Sprite itemImage;
    //public int itemHeld;
    [TextArea]
    public string itemInfo;
}
