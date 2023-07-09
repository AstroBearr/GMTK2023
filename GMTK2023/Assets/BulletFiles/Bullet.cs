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
}