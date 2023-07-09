using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseWin : MonoBehaviour
{
    public GameObject player;
    public Transform humans;


    public GameObject loseScreen;
    public GameObject winScreen;

    private bool gameEnded = false;

    private void Update() {
        if (player == null && gameEnded == false) {
            loseScreen.SetActive(true);
            gameEnded = true;
        }
        if (humans.childCount == 0 && gameEnded == false) {
            winScreen.SetActive(true);
            gameEnded = true;
        }
    }
}
