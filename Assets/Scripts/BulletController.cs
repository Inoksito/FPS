using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float activeTime;

    private float shootTime;
    private int damage;

    public GameObject damageParticle;
    public GameObject impactParticle;

    public int Damage { get => damage; set => damage = value; }

    private void OnEnable()
    {
        shootTime = Time.time;
    }
    private void Update()
    {
        if (Time.time - shootTime>= activeTime)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnDisable()
    {
        gameObject.transform.position = new Vector3(0, -1000, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyController>().DamageEnemy(Damage);
            GameObject particles = Instantiate(damageParticle, transform.position, Quaternion.identity);

        }
        else if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().DamagePlayer(Damage);
            GameObject particles = Instantiate(damageParticle, transform.position,Quaternion.identity);
        }
        else
        {
            GameObject particles = Instantiate(impactParticle, transform.position, Quaternion.identity);
        }
        gameObject.SetActive(false);
    }
}
