using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject player;

    //var
    public float movementSpeed;
    //public GameObject camera;
    public GameObject bulletSpawnPoint;

    public List<GameObject> vfx = new List<GameObject>();
    private GameObject effectToSpawn;

    public int effect;

    public GameObject gameManager;
    
    public GameObject PowerupR;
    public GameObject PowerupY;
    public float waitTime;

    public GameObject playerObject;

    public float points;
    public float maxHealth;
    public float health;

    void Start()
    {
        health = maxHealth;
        effectToSpawn = vfx[effect];
    }


    //metodos
    void Update()
    {
        //jogador a olhar para o mouse
        Plane playerPlane = new Plane(Vector3.up,transform.position);
        Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitDist = 0.0f;

        if(playerPlane.Raycast(ray,out hitDist))
        {
            Vector3 targetPoint = ray.GetPoint(hitDist);
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            targetRotation.x = 0;
            targetRotation.z = 0;
            playerObject.transform.rotation = Quaternion.Slerp(playerObject.transform.rotation,targetRotation,7f * Time.deltaTime);
        }
        //movimento
        if(Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
        }

        if(Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * movementSpeed * Time.deltaTime);
        }

        if(Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);
        }

        if(Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
        }

        //Disparar
        if(Input.GetMouseButtonDown(0))
        {  
              ShootVFX();

        }
        if(health <= 0)
        {
            Died();
        }

    }

     void ShootVFX()
    {
        GameObject vfx;

        if(bulletSpawnPoint != null)
        {
           vfx = Instantiate(effectToSpawn,bulletSpawnPoint.transform.position,playerObject.transform.rotation);
        } else{
            Debug.Log("No spawn point");
        }
    }

    public void getHealth()
    {
        health = maxHealth;
    }

    public void Died()
    {
        gameManager.SendMessage("PlayerDied", SendMessageOptions.DontRequireReceiver);
        player.SetActive(false);
    }

    public void SwitchPowerupR(int PowerUpVFX)
    {
        effectToSpawn = vfx[PowerUpVFX];
        PowerupR.SetActive(true);
        PowerupY.SetActive(false);
    }

    public void SwitchPowerupY(int PowerUpVFX)
    {
        effectToSpawn = vfx[PowerUpVFX];
        PowerupR.SetActive(false);
        PowerupY.SetActive(true);
    }
}
