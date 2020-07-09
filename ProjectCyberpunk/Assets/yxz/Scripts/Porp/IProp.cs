using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProp
{
    //被引爆
    void Detonated();
    //爆炸
    void Explode();
    //对物体经行销毁
    void Destory();
}
