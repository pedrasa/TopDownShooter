using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject restartGame;
    public GameObject startGame;
    public GameObject enemies;
    public GameObject dualEnemies;
    public GameObject boss;

    public GameObject smoke;

    public GameObject fire;

    public GameObject powerUpR;

    public GameObject player;

    public GameObject powerUpY;

    public int startingEnemies;

    public int startingDualEnemies;

    private int deadEnemies;

    private int deadDualEnemies;

    // Start is called before the first frame updat
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    public void StartGame()
    {
      startGame.SetActive(false);
      startingEnemies = 4;
      startingDualEnemies = 3;
      enemies.SetActive(true);
      player.SetActive(true);

    }

    public void EndGame(){
      Application.Quit();
    }

    public void RestartGame()
    {
        enemies.SetActive(true);
        dualEnemies.SetActive(true);
        boss.SetActive(true);
        enemies.SendMessage("enableEnemy", SendMessageOptions.DontRequireReceiver);
        dualEnemies.SendMessage("enableEnemy", SendMessageOptions.DontRequireReceiver);
        boss.SendMessage("enableEnemy", SendMessageOptions.DontRequireReceiver);
        restartGame.SetActive(false);
        deadEnemies = 0;
        deadDualEnemies = 0;
        enemies.SetActive(false);
        dualEnemies.SetActive(false);
        boss.SetActive(false);
        enemies.SetActive(true);
        player.SetActive(true); 
        player.SendMessage("getHealth", SendMessageOptions.DontRequireReceiver);
    }

    public void PlayerDied(){
       restartGame.SetActive(true);
    }
    public void EnemyDied(){
         ++deadEnemies;
    }
    public void BossDied(){
        boss.SetActive(false);
        fire.SetActive(false);
        restartGame.SetActive(true);
    }
    public void DualWieldDied(){
        ++deadDualEnemies;
    }

    public void SwitchPowerUp(int PUP)
    {
       if(PUP ==0)
       {
           powerUpR.SetActive(false);
           powerUpY.SetActive(true);
       }
       if(PUP ==1)
       {
           powerUpR.SetActive(true);
           powerUpY.SetActive(false);
       }
    }

    void Update()
    {
       if(deadEnemies == startingEnemies)
       {
           enemies.SetActive(false);
           dualEnemies.SetActive(true);
           smoke.SetActive(true);
       }
       if(deadDualEnemies == startingDualEnemies)
       {
           smoke.SetActive(false);
           dualEnemies.SetActive(false);
           boss.SetActive(true);
           fire.SetActive(true);
       }

    }
}
