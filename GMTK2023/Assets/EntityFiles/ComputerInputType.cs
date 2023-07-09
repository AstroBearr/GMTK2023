using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerInputType : MonoBehaviour {
    public Entity entity;

    public Transform humans;
    public Transform zombies;
    public Transform bullets;


    public float boidAmount = 0.5f;

    private float spawnOffset;

    public bool began = false;

    //private float[] directions = new float[8];

    public void Awake() {
        if (transform.parent.name == "Zombies") {
            humans = GameObject.Find("Humans").transform;
            zombies = GameObject.Find("Zombies").transform;
        } else {
            humans = GameObject.Find("Zombies").transform;
            zombies = GameObject.Find("Humans").transform;
        }
        bullets = GameObject.Find("Bullets").transform;
        spawnOffset = Time.time + Random.Range(0, entity.reloadtime);
    }

    public void Update() {

        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        bool onScreen = screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;

        if (onScreen) {
            began = true;
        }

        if (began == true) {
            Vector3 targetMoveAdd = new Vector2(0, 0);
            //Transform closestHuman = humans.GetChild(0);
            //float[]
            //foreach(Transform zombie in zombies) {

            //}

            Transform closestHuman = null;
            foreach (Transform human in humans) {
                if (human.GetComponent<ComputerInputType>() != null) {
                    if (human.GetComponent<ComputerInputType>().began == true) {
                        if (closestHuman != null) {
                            if (Vector2.Distance(transform.position, human.position) > Vector2.Distance(transform.position, closestHuman.position)) {
                                closestHuman = human;
                            }
                        } else {
                            closestHuman = human;
                        }
                    }
                } else {
                    if (closestHuman != null) {
                        if (Vector2.Distance(transform.position, human.position) > Vector2.Distance(transform.position, closestHuman.position)) {
                            closestHuman = human;
                        }
                    } else {
                        closestHuman = human;
                    }
                }
            }

            if (closestHuman == null && transform.parent.name == "Zombies") {
                if (Vector2.Distance(transform.position, closestHuman.position) > 3) {
                    //Vector3 unitVector = (closestHuman.position - transform.position) / ((closestHuman.position - transform.position).magnitude);
                    targetMoveAdd = (GameObject.Find("Player Zombie").transform.position - transform.position).normalized;

                }

            
            }

            if (closestHuman != null) {

                if (Vector2.Distance(transform.position, closestHuman.position) > 10) {
                    //Vector3 unitVector = (closestHuman.position - transform.position) / ((closestHuman.position - transform.position).magnitude);
                    targetMoveAdd = (closestHuman.position - transform.position).normalized;

                }

                if (Vector2.Distance(transform.position, closestHuman.position) < 5) {
                    //Vector3 unitVector = (closestHuman.position - transform.position) / ((closestHuman.position - transform.position).magnitude);
                    targetMoveAdd = -(closestHuman.position - transform.position).normalized;

                }
            } 

            foreach (Transform zombie in zombies) {
                float scalingValue = boidAmount - Vector3.Distance(zombie.position, transform.position);
                if (scalingValue < 0) {
                    scalingValue = 0;
                }
                if (zombie != transform && Vector3.Distance(zombie.position, transform.position) < 3)
                    targetMoveAdd += (transform.position - zombie.position) * scalingValue;
                if (zombie != transform && Vector3.Distance(zombie.position, transform.position) < 0.5)
                    targetMoveAdd += (transform.position - zombie.position) * 1f;
            }

            foreach (Transform bullet in bullets) {
                LayerMask layerMask = LayerMask.GetMask("Entity");
                RaycastHit2D hit = Physics2D.Raycast(bullet.position + bullet.right * (bullet.localScale.x / 2), bullet.right, layerMask);
                if (hit.transform == transform) {
                    float scalingValue = boidAmount - Vector3.Distance(bullet.position, transform.position);
                    if (scalingValue < 0) {
                        scalingValue = 0;
                    }
                    Vector3 moveVec;
                    if (Vector3.Distance(bullet.position, transform.position) < 4) {
                        moveVec = (transform.position - bullet.position) * scalingValue;
                        targetMoveAdd += new Vector3(moveVec.y, -moveVec.x, 0);
                        targetMoveAdd += moveVec;
                    }
                    if (Vector3.Distance(bullet.position, transform.position) < 0.5)
                        targetMoveAdd += (transform.position - bullet.position) * 1f;
                }
            }


            entity.Move(targetMoveAdd.normalized);
            if (closestHuman != null) {
                entity.Aim(Mathf.Atan2(closestHuman.position.y - transform.position.y, closestHuman.position.x - transform.position.x));
                if (Time.time > spawnOffset)
                    entity.Fire();
            }
            
        }
    }
}
