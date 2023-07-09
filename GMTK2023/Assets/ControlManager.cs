using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlManager : MonoBehaviour
{
    public float controlPower = 1;
    public float maxControlPower = 1;

    private bool isSelecting = false;
    public bool isControlling = false;
    public Transform controlledHuman;

    public GameObject controlButton;
    public Transform humans;

    public GameObject showBar;
    public GameObject healthBar;
    public GameObject controlledHealthBar;
    public GameObject controlledType;
    public GameObject controlMenu;
    public GameObject selectionEffect;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (isSelecting == false && isControlling == false) {
                isSelecting = true;
                Time.timeScale = 0f;
                CreateButtons();
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            ContinueGame();
            if (isControlling == true) {
                foreach (Transform human in GameObject.Find("Humans").transform) {
                    human.GetComponent<ComputerInputType>().enabled = true;
                    human.GetComponent<PlayerInputType>().enabled = false;
                }
                GameObject.Find("Player Zombie").GetComponent<PlayerInputType>().enabled = true;
                isControlling = false;
            }
        }

        if (isControlling == true && controlledHuman == null) {
            foreach (Transform human in GameObject.Find("Humans").transform) {
                human.GetComponent<ComputerInputType>().enabled = true;
                human.GetComponent<PlayerInputType>().enabled = false;
            }
            GameObject.Find("Player Zombie").GetComponent<PlayerInputType>().enabled = true;
            isControlling = false;
        }

        showBar.GetComponent<Slider>().value = controlPower / maxControlPower;
        healthBar.GetComponent<Slider>().value = (float)GameObject.Find("Player Zombie").GetComponent<Entity>().health / (float)GameObject.Find("Player Zombie").GetComponent<Entity>().maxHealth;
        if (isControlling) {
            controlMenu.SetActive(true);
            controlledHealthBar.GetComponent<Slider>().value = (float)controlledHuman.GetComponent<Entity>().health / (float)controlledHuman.GetComponent<Entity>().maxHealth;
            controlledType.GetComponent<Text>().text = controlledHuman.GetComponent<Entity>().type;
        } else {
            controlMenu.SetActive(false);
        }
    }

    private void CreateButtons() {
        foreach(Transform button in transform.GetChild(7)) {
            Destroy(button.gameObject);
        }
        foreach(Transform human in humans) {
            if (human.GetComponent<Entity>().controlCost <= controlPower) {
                GameObject newObl = Instantiate(controlButton, Camera.main.WorldToScreenPoint(human.position), Quaternion.identity, transform.GetChild(7));
                newObl.GetComponent<ControlButton>().myHuman = human;
                newObl.transform.GetChild(0).GetComponent<Text>().text =  human.GetComponent<Entity>().controlCost.ToString();
            }
        }
        selectionEffect.SetActive(true);
    }

    public void ContinueGame() {
        foreach (Transform button in transform.GetChild(7)) {
            Destroy(button.gameObject);
        }
        isSelecting = false;
        Time.timeScale = 1f;
        selectionEffect.SetActive(false);
    }

}
