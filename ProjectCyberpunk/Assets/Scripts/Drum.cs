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
        
    }

    public void Explode()
    {
        //if (当炸药桶在爆炸的范围之内时)
        //{
        //    anim.SetBool("isBoom", true);
        //}

    }

    public void Destory()
    {
        //Destroy(gameObject);
    }
}
