using UnityEngine;
//抛物弹接口
public interface IBullet {
    void Shoot(Transform shootPosition, Vector3 mousePosition, float force, Transform rotateCenter);
    //void Shoot(Transform shootPosition);

    
}
