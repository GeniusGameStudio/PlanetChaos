using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameramove : MonoBehaviour
{
    private Vector3 offset;
    public Transform player;


    void Start()
    {
        offset = player.position - transform.position;
    }


    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, player.position - offset, Time.deltaTime * 5);
        Quaternion rotation = Quaternion.LookRotation(player.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 3f);
    }
}
