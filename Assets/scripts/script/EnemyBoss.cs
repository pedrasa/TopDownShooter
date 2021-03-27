using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : MonoBehaviour
{
    //var
    public float health;
    public bool shot;
    public GameObject player;
    public float pointsToGive;
   
    private float initialHealth;

    public GameObject impactPrefab;
    public GameObject diePrefab;
    private float currentWaitTime;
    public float waitTime;
    public GameObject bullet;

    public GameObject bullet1;
    private Transform pistolHolder;
    private Transform pistolHolder1;
    private Transform pistolHolder2;
    public Transform bulletSpawnPoint;
    public Transform bulletSpawnPoint1;
    public Transform bulletSpawnPoint2;

    public GameObject gameManager;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        pistolHolder = this.transform.GetChild(0);
        pistolHolder1 = this.transform.GetChild(1);
        pistolHolder2 = this.transform.GetChild(2);
        bulletSpawnPoint = pistolHolder.GetChild(2);
        bulletSpawnPoint1 = pistolHolder1.GetChild(2);
        bulletSpawnPoint2 = pistolHolder2.GetChild(2);
        initialHealth = health;
    }

    //metodos
    public void Update()
    {
        if(!bulletSpawnPoint)
        {
            bulletSpawnPoint = pistolHolder.GetChild(2);
        }
        if(!bulletSpawnPoint1)
        {
            bulletSpawnPoint1 = pistolHolder1.GetChild(2);
        }
        if(!bulletSpawnPoint2)
        {
            bulletSpawnPoint2 = pistolHolder2.GetChild(2);
        }
        if (health <= 0)
        {
            Die();
        }

        this.transform.LookAt(player.transform);
        if(currentWaitTime == 0)
        {
            shoot();
        }
        if(shot && currentWaitTime < waitTime)
        {
            currentWaitTime += Time.deltaTime;
        }
        if(currentWaitTime >= waitTime)
        {
            currentWaitTime = 0;
        }
    }

    public void Die()
    {
        player = GameObject.FindWithTag("Player");
        gameManager.SendMessage("BossDied", SendMessageOptions.DontRequireReceiver);
        this.gameObject.SetActive(false);
        if(diePrefab != null)
        {
            var dieVFX = Instantiate(diePrefab,transform.position,this.transform.rotation);
            dieVFX.transform.forward = gameObject.transform.forward;
            var psdie = dieVFX.GetComponent<ParticleSystem>();
            if(psdie!=null)
            {
                Destroy(dieVFX, psdie.main.duration);
            }
            else
            {
                var psChild = dieVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(dieVFX, psChild.main.duration);
            }
        }
        player.GetComponent<Player>().points += pointsToGive;
        health = initialHealth;
    }

    public void shoot()
    {
       shot = true;
       Instantiate(bullet.transform,bulletSpawnPoint.transform.position,this.transform.rotation);
       Instantiate(bullet.transform,bulletSpawnPoint1.transform.position,this.transform.rotation);
       Instantiate(bullet1.transform,bulletSpawnPoint2.transform.position,this.transform.rotation);
       
    }

    public void ApplyDamage(int damage)
    {
        health -= damage;
        if(impactPrefab != null)
        {
            var impactVFX = Instantiate(impactPrefab,transform.position,this.transform.rotation);
            impactVFX.transform.forward = gameObject.transform.forward;
            var psimpact = impactVFX.GetComponent<ParticleSystem>();
            if(psimpact!=null)
            {
                Destroy(impactVFX, psimpact.main.duration);
            }
            else
            {
                var psChild = impactVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(impactVFX, psChild.main.duration);
            }
        }
    }

    public void getHealth()
    {
        health = initialHealth;
    }
}
