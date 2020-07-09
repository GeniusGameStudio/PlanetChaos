using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBag : MonoBehaviour
{
    
    public GameObject Bag;
    public GameObject information;

    void Update()
    {
        Open();
    }

    //按下键盘上的B对背包进行打开和关闭
    public void Open()
    {
        
        if (Input.GetKeyDown(KeyCode.B))
        {
            
            Bag.SetActive(!Bag.activeSelf);
            information.SetActive(false);
        }
    }
}
