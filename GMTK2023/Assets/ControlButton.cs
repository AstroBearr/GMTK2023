using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlButton : MonoBehaviour
{
    public Transform myHuman;

    public void OnClick() {
        myHuman.GetComponent<ComputerInputType>().enabled = false;
        myHuman.GetComponent<PlayerInputType>().enabled = true;

        GameObject.Find("Player Zombie").GetComponent<PlayerInputType>().enabled = false;

        GameObject.Find("Canvas").GetComponent<ControlManager>().controlledHuman = myHuman;
        GameObject.Find("Canvas").GetComponent<ControlManager>().ContinueGame();
        GameObject.Find("Canvas").GetComponent<ControlManager>().isControlling = true;
        GameObject.Find("Canvas").GetComponent<ControlManager>().controlPower -= myHuman.GetComponent<Entity>().controlCost;
    }
}
