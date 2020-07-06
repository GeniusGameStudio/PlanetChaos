using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlClass : MonoBehaviour, IBullet {
    public float subForce;
    public bool over;
    
    // 手动控制子弹（未实现）
    public void Shoot(Transform shootPosition, Vector3 mousePosition, float force, Transform rotateCenter) {
        if (!over) {
            GameObject bullet = Instantiate(gameObject, shootPosition.position, shootPosition.rotation);

            Rigidbody2D rig = bullet.GetComponent<Rigidbody2D>();
            rig.velocity = rotateCenter.right * force;
            if (Input.GetMouseButton(0)) {
                Debug.Log("Control");
                rig.gravityScale = 0;
                rig.velocity = (mousePosition - transform.position).normalized * (force * subForce);
                over = true;
            }
        }
    }
}
