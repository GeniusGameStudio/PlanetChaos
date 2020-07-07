using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

// Mount to RotatePosition;
// 挂载给旋转中心。

// Player => RotatePosition(手臂旋转中心) => 准星 => 枪口
//主方法类

public class ParaCurve : MonoBehaviour {
    public Slider slider; // 蓄力条
    private Vector2 _playerPosition; //player的位置
    private Vector2 _mousePosition;  // 光标位置
    private int _bulletsIndex;  //子弹索引角标
    public GameObject []bullets;  // 子弹列表
    public float shootForce;  // 抛物弹射击力度
    public Transform shootPosition;  // 发射口位置
    public bool isOnShoot; // 抛物弹是否发射
    private float _force; // 抛物弹射击力度副本
    public LineRenderer lineRenderer;  // 射线预制体
    private Radial _radial;  // 射线脚本
    private BulletClass _bulletClass;  // 抛物弹脚本
    private ControlClass _controlClass;  // 手动控制但脚本
    public GameObject enemy;  // 敌人
    private TrackBullet _trackBullet;  // 追踪弹脚本
    private Rigidbody2D _trackBulletRig;  // 追踪弹刚体
    
    private void Awake() {
        //_shootPosition = transform.Find("ShootPosition");
        _force = shootForce;  // 保存力度副本
        _radial = lineRenderer.GetComponent<Radial>(); // 得到射线脚本
        _bulletClass = bullets[0].GetComponent<BulletClass>(); // 得到抛射弹脚本
        _trackBullet = bullets[2].GetComponent<TrackBullet>(); // 得到追踪弹脚本
        _trackBulletRig = bullets[2].GetComponent<Rigidbody2D>();  // 追踪弹刚体
    }

    void Update() {
        _playerPosition = transform.position;  
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);  // 获取世界坐标系下鼠标的位置
        transform.right = (_mousePosition - _playerPosition);

        if (Input.GetKeyDown(KeyCode.Space)) { // 当按下空格时切换弹药
            _bulletsIndex += 1;
            _bulletsIndex %= bullets.Length;
        }

        

        switch (_bulletsIndex) {  
            case 0: 
                _radial.Close();  //关闭射线
                Shoot(_bulletClass);  // 发射抛物弹
                _bulletClass.ChangeRotation();
                break;

            case 1: {
                _radial.Show(shootPosition.position, _mousePosition);  // 开启射线，
                if (Input.GetMouseButtonDown(0)) {  // 当按下左键开始射线检测
                    _radial.Check();    
                }
                break;
            }
            case 2: {
                _radial.Close();  // 关闭射线
                Shoot(_trackBullet);  //发射追踪弹
                _trackBullet.ChangeRotation();
                if (_trackBulletRig != null && enemy) // 如果有敌人，则开始追踪
                {
                    _trackBullet.CheckPosition(enemy, _trackBulletRig);
                    _trackBullet.ChangeRotation();
                }
                
                break;
            }
                
        }
        
    }


    //发射追踪弹
    void Shoot(TrackBullet trackBullet) {
        BuildUpStrength();  // 蓄力
        if (isOnShoot && Input.GetMouseButtonUp(0) ) {  // 发射
            if(enemy)
                _trackBulletRig = trackBullet.Shoot(enemy, shootPosition, _mousePosition, _force, transform); 
            _force = shootForce;  // 归零
            slider.value = 0;  // 归零
        }

        

    }
    
    

    
    //发射抛物弹
    void Shoot(IBullet bulletInterface) {
        BuildUpStrength();  // 蓄力
        if (isOnShoot && Input.GetMouseButtonUp(0)) {  // 蓄力完成
            bulletInterface.Shoot(shootPosition, _mousePosition, _force, transform);
            _force = shootForce;  // 重置
            slider.value = 0;  // 重置
            /*if (_bulletsIndex == 0) {
                GameObject b = Instantiate(bullets[_bulletsIndex], shootPosition.position, shootPosition.rotation);
                b.GetComponent<Rigidbody2D>().velocity = transform.right * _force;
                _force = shootForce;
            }else if (_bulletsIndex == 1) {
                GameObject b = Instantiate(bullets[_bulletsIndex], shootPosition.position, shootPosition.rotation);
                b.GetComponent<Rigidbody2D>().gravityScale = 0;
                b.GetComponent<Rigidbody2D>().velocity = transform.right * _force;
                
                _force = shootForce;
              */


        }
    }

    
    //蓄力
    void BuildUpStrength() {
        if (Input.GetMouseButton(0)) {
            
            isOnShoot = true;
            _force += 0.02f;
            slider.value = (float) ((_force - 10) / 5);
            if (_force >= 15) _force = 15;
            
        }
    }

        
}

