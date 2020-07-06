using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//射线类
public class Radial : MonoBehaviour, IRadial {
    private LineRenderer _line;  // 射线
    private RaycastHit2D _hit;   // 射线检测
    void Awake() {
        _line = GetComponent<LineRenderer>();  // 初始化
    }

    //开启射线
    public void Show(Vector3 shootPosition, Vector3 mousePosition) { // 发射点，鼠标点
        _hit = Physics2D.Raycast((Vector2)shootPosition, (mousePosition - shootPosition).normalized);   //射线检测
        _line.enabled = true;  // 开启射线
        _line.useWorldSpace = true;  
        
        _line.SetPosition(0, shootPosition);  //设置射线起点

        if (_hit) {  // 如果检测到
            _line.SetPosition(1, _hit.point);  // 设置终点为检测到的物体的表面
        }
        else {  // 未检测到，终点到起点长度为10
            _line.SetPosition(1, (mousePosition - shootPosition).normalized * 10);
        }
    }

    //检测射中的物体的标签
    public void Check() {
        if ( _hit  && _hit.transform.CompareTag("Player")  ) {
            Destroy(_hit.transform.gameObject);
        }
    

    }

    /*
    public void Shoot(Vector3 shootPosition, Vector3 mousePosition) {
        
        RaycastHit2D hit = Physics2D.Raycast((Vector2)shootPosition, (mousePosition - shootPosition).normalized);

        
        
        
        if (hit) {
            _line.enabled = true;
            _line.useWorldSpace = true;
        
            _line.SetPosition(0, shootPosition);
            
            if (hit.transform.CompareTag("Enemy")) {
                _line.SetPosition(1, hit.point);
            }
            else {
                _line.SetPosition(1, (mousePosition - shootPosition).normalized);
            }
        }
        
        
        



    }

    */
    
    //关闭射线

    public void Close() {
        _line.enabled = false;
        _line.useWorldSpace = false;
    }

    
}
