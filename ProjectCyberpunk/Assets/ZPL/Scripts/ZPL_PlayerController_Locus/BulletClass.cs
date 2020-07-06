using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletClass : MonoBehaviour, IBullet {
    
    // 抛射弹类
    public void Shoot(Transform shootPosition, Vector3 mousePosition, float force, Transform rotateCenter) {
        GameObject bullet = Instantiate(gameObject, shootPosition.position, shootPosition.rotation); // 生成子弹，以发射点的旋转和位置生成
        Rigidbody2D rig = bullet.GetComponent<Rigidbody2D>();  //获取刚体组件
        rig.velocity = rotateCenter.right * force;  // 给刚体组件添加速度，来模拟抛物线

    }

    
}
