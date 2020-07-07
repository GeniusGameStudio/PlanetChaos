using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //RotationCenter`Transform
    private Transform _rotationCenter;
    //发射力度
    public float shootForce = 10.0f;
    //蓄力弹最大蓄力时间
    public float maxShootTime;
    //当前蓄力时间
    private float _shootTime;
    //子弹列表
    public GameObject[] bullets;  // 1 SimpleBullet, 2 Radial Bullet, 3 
    //射线Render
    public LineRenderer lineRenderer;
    //子弹列表角标
    private int _bulletsIndex;
    //发射力度副本
    private float _force;
    //旋转点位置
    private Vector2 _playerPosition;
    //鼠标位置
    private Vector2 _mousePosition;
    
    
    // Start is called before the first frame update
    void Start() {
        _rotationCenter = transform.Find("RotationCenter");
        _force = shootForce;
        
    }

    // Update is called once per frame
    void Update()
    {
        _playerPosition = transform.position;  // 初始化旋转点位置
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);  // 获取世界坐标系下鼠标的位置
        transform.right = (_mousePosition - _playerPosition); // 改变旋转点方向
        
        
        
    }

    void SwitchBullet() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            _bulletsIndex++;
            _bulletsIndex %= bullets.Length;
        }
    }
    
    
    
    
}
