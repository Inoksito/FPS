using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    private int currentLife;
    private int maxLife;
    public int enemyScorePoint;
    public EnemyData enemyData;
    private Renderer enemyRenderer;

    public Transform[] patrolPoints;
    private int destPoint = 0;

    private PlayerController target;
    private NavMeshAgent agent;
    private WeaponController weaponController;

    private void Start()
    {
        target= FindObjectOfType<PlayerController>();
        agent = GetComponent<NavMeshAgent>();
        weaponController = GetComponent<WeaponController>();
        agent.autoBraking = false;
        GoToNextPoint();

        //Data from Scriptable Object
        agent.speed = enemyData.Speed; 
        currentLife = enemyData.MaxLife;
        maxLife = enemyData.MaxLife;
        enemyRenderer = GetComponent<Renderer>();
        enemyRenderer.material = enemyData.EnemyMaterial;


    }

    private void GoToNextPoint()
    {
        if (patrolPoints.Length == 0)
        {
            return;
        }
        agent.destination = patrolPoints[destPoint].position;

        destPoint= (destPoint+1) % patrolPoints.Length;
    }
    
    private void Update()
    {
        SearchEnemy();
       if(!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GoToNextPoint();
        }
    }

    private void SearchEnemy()
    {
        NavMeshHit hit;
        if (!agent.Raycast(target.transform.position, out hit)) { 

            if (hit.distance <= 20f)
            {
                agent.stoppingDistance = 6;
                agent.SetDestination(target.transform.position);
                agent.autoBraking = true;
                agent.transform.LookAt(target.transform.position);
                if(hit.distance <= 8)
                {
                    if (weaponController.CanShoot())
                    {
                        weaponController.Shoot();
                    }
                   
                }
            }
            else
            {
                agent.stoppingDistance = 0;
            }
    }
        else
        {
            agent.stoppingDistance = 0;
        }


    }

    public void DamageEnemy(int quantity)
    {
        currentLife -= quantity;
        if (currentLife <= 0 )
        {
            GameManager.instance.UpdateScore(100);
            Destroy(gameObject);
        }
    }
}
