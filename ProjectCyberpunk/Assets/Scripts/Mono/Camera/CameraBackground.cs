using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBackground : MonoBehaviour
{
    public Camera followCamera;
    
    void Start()
    {
        
    }

    void Update()
    {
        transform.position = new Vector3(followCamera.transform.position.x, followCamera.transform.position.y, transform.position.z);
    }
}
