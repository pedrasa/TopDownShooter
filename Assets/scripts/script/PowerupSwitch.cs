using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSwitch : MonoBehaviour
{

    public GameObject triggeringPlayer;

    public GameObject gameManager;
    public void OnTriggerEnter(Collider player)
    {
        Debug.Log("Bruh");
        triggeringPlayer = player.gameObject;
        if(this.tag ==  "PowerupRed" && player.tag == "Player")
        {
            triggeringPlayer.gameObject.SendMessage("SwitchPowerupR", 0, SendMessageOptions.DontRequireReceiver);
            gameManager.SendMessage("SwitchPowerUp", 0, SendMessageOptions.DontRequireReceiver);
        }
        if(this.tag == "PowerupYellow" && player.tag == "Player")
        {
            triggeringPlayer.gameObject.SendMessage("SwitchPowerupY", 1, SendMessageOptions.DontRequireReceiver);
            gameManager.SendMessage("SwitchPowerUp", 1, SendMessageOptions.DontRequireReceiver);
        }
    }
}
