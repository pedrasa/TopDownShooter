using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //var
    public float health;
    private float initialHealth;
    public bool shot;
    public GameObject player;
    public float pointsToGive;


    public GameObject impactPrefab;
    public GameObject diePrefab;
    private float currentWaitTime;
    public float waitTime;
    public GameObject bullet;
    private Transform pistolHolder;
    public Transform bulletSpawnPoint;

    public GameObject gameManager;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        pistolHolder = this.transform.GetChild(0);
        bulletSpawnPoint = pistolHolder.GetChild(2);
        initialHealth = health;
    }

    //metodos
    public void Update()
    {
        if(!bulletSpawnPoint)
        {
            bulletSpawnPoint = pistolHolder.GetChild(2);
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
        gameManager.SendMessage("EnemyDied", SendMessageOptions.DontRequireReceiver);
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
