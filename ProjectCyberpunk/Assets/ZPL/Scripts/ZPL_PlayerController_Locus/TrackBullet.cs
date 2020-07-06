using UnityEngine;
//追踪弹类
public class TrackBullet : MonoBehaviour {
    public Vector2 offset;  // 自生位置和目标位置的偏移

    public Rigidbody2D Shoot(GameObject position, Transform shootPosition, Vector3 mousePosition, float force,
        Transform rotateCenter) {
        GameObject trackBullet = Instantiate(gameObject, shootPosition.position, shootPosition.rotation);
        Rigidbody2D rig = trackBullet.GetComponent<Rigidbody2D>();
        rig.velocity = rotateCenter.right * force;
        return rig;
        
    }
    //追踪
    public void CheckPosition(GameObject enemy, Rigidbody2D rig) {
       
        if (rig.velocity.y <= 0) { // 当竖直速度小于0时开始追踪目标（如果存在），
            Debug.Log(offset);
            offset = (enemy.transform.position - rig.transform.position).normalized;
            //rig.velocity = offset * 10;
            rig.velocity = offset * 9;
        }
    }
}
