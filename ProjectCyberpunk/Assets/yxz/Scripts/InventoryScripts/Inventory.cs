using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/New Iventory")]
public class Inventory : ScriptableObject
{
    public List<Item> itemList = new List<Item>();
    public List<int> itemNum = new List<int>();
}
