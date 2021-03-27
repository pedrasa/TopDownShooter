using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableEnemies : MonoBehaviour
{
    // Start is called before the first frame update
    public void enableEnemy()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
          transform.GetChild(i).gameObject.SetActive(true);
          transform.GetChild(i).gameObject.SendMessage("getHealth", SendMessageOptions.DontRequireReceiver);
          
        }
    }
}
