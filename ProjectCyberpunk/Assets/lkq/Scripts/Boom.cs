using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    public GameObject preFab;
    bool BoomCreat()
    {
        GameObject.Instantiate(preFab,transform.position,transform.rotation);
        return true;
    }
    private void Update()
    {
        if(Input.GetKey(KeyCode.Space)){//

            BoomCreat(); 
            } 
    }
     
}
