using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour, IProp
{
    private BoxCollider2D coll;
    private Rigidbody2D rig;
    private Animator anim;
    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Detonated();
    }
    //当陷阱被玩家炸弹引爆
    public void Detonated()
    {
        //当陷阱的位置在玩家炸弹的范围内时爆炸
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

    //当人物触碰到地刺陷阱时会对人物造成伤害
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //对玩家造成伤害
        }
    }

    public void Destory()
    {
        Destroy(gameObject);
    }

}
