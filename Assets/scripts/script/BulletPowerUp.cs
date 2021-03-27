using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPowerUp : MonoBehaviour
{
     public float bulletSpeed;
    public float maxDistance;
    public float maxDistanceFinal;

    public GameObject muzzlePrefab;
    // Start is called before the first frame update

    public GameObject triggeringEnemy;

    public float damage;
    public GameObject player;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        if(muzzlePrefab != null)
        {
             Debug.Log("bulletFlash");
            var muzzleVFX = Instantiate(muzzlePrefab,transform.position,player.transform.rotation);
            muzzleVFX.transform.forward = gameObject.transform.forward;
            var psMuzzle = muzzleVFX.GetComponent<ParticleSystem>();
            if(psMuzzle!=null)
            {
                Destroy(muzzleVFX, psMuzzle.main.duration);
            }
            else
            {
                var psChild = muzzleVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(muzzleVFX, psChild.main.duration);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * bulletSpeed );
        maxDistance += 1 * Time.deltaTime;

        if(maxDistance >= maxDistanceFinal)
        {
            Destroy(this.gameObject);
        }
    }

    public void OnTriggerEnter(Collider enemy)
    {
        if(enemy.tag ==  "Enemy")
        {
            triggeringEnemy = enemy.gameObject;
            triggeringEnemy.SendMessage("ApplyDamage", damage);
            //triggeringEnemy.GetComponent<Enemy>().health -= damage;
            Destroy(this.gameObject);
        }
        if(enemy.tag ==  "Player")
        {
            player.GetComponent<Player>().health -= 20;
        }
    }
}
