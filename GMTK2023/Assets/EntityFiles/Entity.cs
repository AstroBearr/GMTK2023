using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform gun;
    public GameObject bullet;
    public float speed = 1;
    public float gunDistance = 1;
    public float reloadtime = 1;

    public float gunOffset = 0.5f;

    public int health = 100;
    public int maxHealth = 100;

    private float reload = 0;

    public int controlCost = 1;

    private Vector3 lastFramePos = new Vector3(0, 0, 0);

    public string type = "Pistol Man";
    public int damage = 15;
    public float bulletSpeed = 1;

    public void Move(Vector2 moveVector) {
        Vector2 outPos = new Vector2(transform.position.x, transform.position.y) + (moveVector.normalized * speed * Time.deltaTime);
        rb.MovePosition(outPos);
        if (transform.position != lastFramePos)
            transform.GetComponent<Animator>().SetFloat("Speed", speed);
    }

    public void Aim(float angle) {
        float x = gunDistance * Mathf.Cos(angle);
        float y = gunDistance * Mathf.Sin(angle);

        gun.localPosition = new Vector2(x, y + gunOffset);
        gun.eulerAngles = new Vector3(0, 0, angle * Mathf.Rad2Deg);

        if (angle * Mathf.Rad2Deg > 90 || angle * Mathf.Rad2Deg < -90)
            gun.localScale = new Vector3(1, -1, 1);
        else
            gun.localScale = new Vector3(1, 1, 1);
    }

    public void Update() {
        reload -= Time.deltaTime;
        if (lastFramePos == transform.position) {
            transform.GetComponent<Animator>().SetFloat("Speed", speed);
        }
        lastFramePos = transform.position;
    }

    public void Fire() {
        if (reload <= 0) {
            GameObject newBullet = GameObject.Instantiate(bullet, gun.GetChild(0).position, gun.rotation);
            newBullet.transform.parent = GameObject.Find("Bullets").transform;
            newBullet.GetComponent<Bullet>().damage = damage;
            reload = reloadtime;
            transform.GetComponent<Animator>().SetTrigger("Attack");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Bullet") {
            health -= collision.gameObject.GetComponent<Bullet>().damage;
            Destroy(collision.gameObject);
        }
        if (health <= 0) {
            Destroy(gameObject);
        }
    }
}
