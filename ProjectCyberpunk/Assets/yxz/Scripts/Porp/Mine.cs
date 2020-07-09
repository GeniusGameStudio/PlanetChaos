using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour, IProp
{
    private BoxCollider2D coll;
    private Animator anim;
    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Detonated();
    }

    //当地雷在爆炸范围内时爆炸
    public void Detonated()
    {
        //当油桶的位置在玩家炸弹的范围内时爆炸
        /*if ()
        {
            Explode();
        }*/
    }


    //当人物碰到地雷就爆炸
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Explode();
        }
    }


    //爆炸
    public void Explode()
    {
        //爆炸破坏地形，对范围内的人物造成伤害

        anim.SetBool("isExplode", true);
    }
    //销毁
    public void Destory()
    {
        Destroy(gameObject);
    }
}
