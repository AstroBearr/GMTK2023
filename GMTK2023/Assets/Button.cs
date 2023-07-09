using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public string type;

    public void Pressed() {
        if (type == "retry") {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (type == "menu") {
            SceneManager.LoadScene(0);
        }
    }
}
