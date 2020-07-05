using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ray : MonoBehaviour
{
    public GameObject prefab;
    int startnumber=0;
    public int number=3;
    RaycastHit2D hit;
    void Start()
    {
        while (true) {
            hit=Physics2D.Raycast(transform.position, new Vector2(transform.position.x + Random.Range(-15, 15), transform.position.y - 10));
            if (hit.transform) {
            if(hit.transform.tag == "background"){
                Debug.Log(hit.transform.tag);
                GameObject.Instantiate(prefab,new Vector2(hit.point.x,hit.point.y+2),new Quaternion(0,0,0,0));
                    startnumber++;
            } }
            if (startnumber == number)
            {
                break;
            }
        }
       
    }
    void Update()
    {
         Debug.DrawRay(transform.position, new Vector2(transform.position.x + Random.Range(-15, 15), transform.position.y - 10));
        

    }
}
