using UnityEngine;


//敌人类
public class Boss : MonoBehaviour
{
    //当发生碰撞时检测是否为子弹，如果时则销毁，可以更改为减少生命值等
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.transform.CompareTag("Bullet")) {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }

        
    
}
}
