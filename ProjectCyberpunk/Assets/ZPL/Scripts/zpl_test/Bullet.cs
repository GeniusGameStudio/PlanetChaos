using System;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public BulletPoll bulletPoll;
    
    void Shoot(Transform shootPosition, Vector3 mousePosition, float force, Transform rotateCenter) {
        GameObject bullet = bulletPoll.SpawnObject(shootPosition.position, shootPosition.rotation);
        Rigidbody2D rig = bullet.GetComponent<Rigidbody2D>();
        rig.velocity = rotateCenter.right * force;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Enemy")) {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}