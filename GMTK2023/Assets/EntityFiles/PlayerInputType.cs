using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputType : MonoBehaviour
{
    public Entity entity;

    private void Update() {
        entity.Move(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));
        Vector2 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        entity.Aim(Mathf.Atan2(worldMousePos.y - transform.position.y, worldMousePos.x - transform.position.x));

        if (Input.GetAxisRaw("Fire1") == 1) {
            entity.Fire();
        }
    }
}
