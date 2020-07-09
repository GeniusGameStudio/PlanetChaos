using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Drum : MonoBehaviour ,IProp
{
    private Rigidbody2D rb;
    private Collider2D coll;
    private Animator anim;
   
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Detonated();
    }

    //当油桶被玩家炸弹引爆
    public void Detonated()
    {
        //当油桶的位置在玩家炸弹的范围内时爆炸
        /*if ()
        {
            Explode();
        }*/
    }

    public void Explode()
    {
        //爆炸破坏地形，对范围内的人物造成伤害

        anim.SetBool("isExplode", true);
    }

    public void Destory()
    {
        Destroy(gameObject);
    }
}
