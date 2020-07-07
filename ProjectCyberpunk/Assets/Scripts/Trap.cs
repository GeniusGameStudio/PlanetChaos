using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private BoxCollider2D coll;
    private Rigidbody2D rig;
    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        rig = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
    }
}
