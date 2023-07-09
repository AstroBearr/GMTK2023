using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    public void OnClick() {
        SceneManager.LoadScene("Level "+gameObject.name);
    }
}
