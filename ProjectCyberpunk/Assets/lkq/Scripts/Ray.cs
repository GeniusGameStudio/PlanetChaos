using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ray : MonoBehaviour       //利用射线检测随机生成物体脚本
{
    public GameObject preFab;         //需要生成的道具（预制体）
    int startNumber=0;                //生成物体数量的初始值
    public int number=3;              //所需生成的物体数量
    RaycastHit2D hit;
    void Start()
    {
        while (true) {
            hit=Physics2D.Raycast(transform.position, new Vector2(transform.position.x + Random.Range(-15, 15), transform.position.y - 10));//对反射源下方扇形区域随机发射射线，获取碰撞到的物体信息
            if (hit.transform) {//如果存在碰撞到的物体，不存在重新检测
            if(hit.transform.tag == "background"){//检查标签是否为background
                Debug.Log(hit.transform.tag);//debug确认标签
                GameObject.Instantiate(preFab,new Vector2(hit.point.x,hit.point.y+2),new Quaternion(0,0,0,0));//则在碰撞点上方生成预制体
                    startNumber++;//初始值加一，限制生成数量
            } }
            if (startNumber == number)//当生成数量达标时停止发射射线
            {
                break;
            }
        }
       
    }
    void Update()
    {
         Debug.DrawRay(transform.position, new Vector2(transform.position.x + Random.Range(-15, 15), transform.position.y - 10));//debug方便观察扇形区域
        

    }
}
