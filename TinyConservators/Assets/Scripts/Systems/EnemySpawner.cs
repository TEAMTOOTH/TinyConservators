using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float spawnRate;
    [SerializeField] float spawnPeriod;
    [SerializeField] GameObject[] enemiesToSpawn;
    [SerializeField] GameObject[] enemySpawnPoints;


    private void Start()
    {
        //Not the cleanest implementation of this, but it works for now
        var sp = GetComponentsInChildren<SpawnPoint>();
        enemySpawnPoints = new GameObject[sp.Length];
        for(int i = 0; i < sp.Length; i++)
        {
            enemySpawnPoints[i] = sp[i].gameObject;
        }
    }

    //Not a great method, just used for testing right now 15.04.2025 - Seb
    public void StartSpawning()
    {
        IndexRandomizer indexRandom = new IndexRandomizer();
        StartCoroutine(Spawn());
        IEnumerator Spawn()
        {
            yield return new WaitForSeconds(3); //BADBADBAD, just for test

            float timePassed = 0;
            float timeSinceLastSpawn = 0;
            int enemyIndex = 0;
            int positionIndex = 0;

            while(timePassed < spawnPeriod)
            {
                timePassed += Time.deltaTime;
                timeSinceLastSpawn += Time.deltaTime;

                if(timeSinceLastSpawn > spawnRate)
                {
                    enemyIndex = indexRandom.GetNewIndex(enemiesToSpawn.Length, enemyIndex);
                    positionIndex = indexRandom.GetNewIndex(enemySpawnPoints.Length, positionIndex);

                    Instantiate(enemiesToSpawn[enemyIndex], enemySpawnPoints[positionIndex].transform.position, Quaternion.identity);
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
