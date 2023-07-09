using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public Rigidbody2D rb;

    public float speed = 1;
    public int damage = 1;

    private void Awake() {
        rb.AddRelativeForce(new Vector2(speed, 0));
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.gameObject.CompareTag("Wall")) {
            Destroy(gameObject);
        }
    }
}