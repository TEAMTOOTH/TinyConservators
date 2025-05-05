using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float spawnRate;
    [SerializeField] float spawnPeriod;
    [SerializeField] GameObject enemyToSpawn;

    
    

    //Not a great method, just used for testing right now 15.04.2025 - Seb
    public void StartSpawning()
    {
        StartCoroutine(Spawn());
        IEnumerator Spawn()
        {
            yield return new WaitForSeconds(3); //BADBADBAD, just for test

            float timePassed = 0;
            float timeSinceLastSpawn = 0;

            while(timePassed < spawnPeriod)
            {
                timePassed += Time.deltaTime;
                timeSinceLastSpawn += Time.deltaTime;

                if(timeSinceLastSpawn > spawnRate)
                {
                    Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
                    timeSinceLastSpawn = 0;
                    
                }
                yield return null;
            }
        }
    }

    public void EndSpawning()
    {

    }
}
