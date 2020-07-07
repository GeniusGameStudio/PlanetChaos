using UnityEngine;
//追踪弹类
public class TrackBullet : MonoBehaviour {
    public Vector2 offset;  // 自生位置和目标位置的偏移
    private GameObject _trackBullet;
    private Rigidbody2D _rig;
    public Rigidbody2D Shoot(GameObject position, Transform shootPosition, Vector3 mousePosition, float force,
        Transform rotateCenter) {
        _trackBullet = Instantiate(gameObject, shootPosition.position, shootPosition.rotation);
        _rig = _trackBullet.GetComponent<Rigidbody2D>();
        _rig.velocity = rotateCenter.right * force;
        return _rig;
        
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
    
    public void ChangeRotation() {
        if(_rig != null)
            _trackBullet.transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(_rig.velocity.y, _rig.velocity.x) * Mathf.Rad2Deg, Vector3.forward);
    }
}
