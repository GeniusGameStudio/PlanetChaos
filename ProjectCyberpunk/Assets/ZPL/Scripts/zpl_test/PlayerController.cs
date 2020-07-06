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
    public GameObject[] bullets;
    //射线Render
    public LineRenderer lineRenderer;
    //子弹列表角标
    private int _bulletsIndex;
    //发射力度副本
    private float _force;
    
    
    
    
    // Start is called before the first frame update
    void Start() {
        _rotationCenter = transform.Find("RotationCenter");
        _force = shootForce;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
