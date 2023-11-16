using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private int enemyNumber;
    [SerializeField] private GameObject enemyObject;

    private Coroutine spawnCoroutine;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && spawnCoroutine==null)
        {
            spawnCoroutine = StartCoroutine(SpawnEnemy());
        }
    }

    private IEnumerator SpawnEnemy()
    {
        InstantiateEnemy();
        yield return new WaitForSeconds(1.5f);
        if((GameObject.FindGameObjectsWithTag("Enemy").Length >= enemyNumber))
        {

        }
    }

    private void InstantiateEnemy()
    {
       Vector3 spawnPosition = RandomNavmeshLocation(5);
       Instantiate(enemyObject,spawnPosition,Quaternion.identity);
    }

    private Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        do { 
            if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
            {
                finalPosition = hit.position;
            }
        }while (finalPosition == Vector3.zero);
        return finalPosition;
    }
}
