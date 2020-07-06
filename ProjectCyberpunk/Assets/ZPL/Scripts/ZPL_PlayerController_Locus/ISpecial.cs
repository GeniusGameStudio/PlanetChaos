

using UnityEngine;
// 特殊子弹接口
public interface ISpecial {
    void Shoot(GameObject enemy, Transform shootPosition, Vector3 mousePosition, float force, Transform rotateCenter);
    
}
