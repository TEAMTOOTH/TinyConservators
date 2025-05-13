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

    public void SpawnEnemies(int amount)
    {
        IndexRandomizer indexRandom = new IndexRandomizer();

        //Giving this an initial randomization, so that they can spawn in any position as their first one.
        int enemyIndex = Random.Range(0, enemiesToSpawn.Length);
        int positionIndex = Random.Range(0, enemySpawnPoints.Length);
        

        for (int i = 0; i < amount; i++)
        {
            enemyIndex = indexRandom.GetNewIndex(enemiesToSpawn.Length, enemyIndex);
            positionIndex = indexRandom.GetNewIndex(enemySpawnPoints.Length, positionIndex);

            Instantiate(enemiesToSpawn[enemyIndex], enemySpawnPoints[positionIndex].transform.position, Quaternion.identity);
        }
    }

    public void SpawnEnemies(int amount, GameObject owner)
    {
        IndexRandomizer indexRandom = new IndexRandomizer();

        //Giving this an initial randomization, so that they can spawn in any position as their first one.
        int enemyIndex = Random.Range(0, enemiesToSpawn.Length);
        int positionIndex = Random.Range(0, enemySpawnPoints.Length);


        for (int i = 0; i < amount; i++)
        {
            enemyIndex = indexRandom.GetNewIndex(enemiesToSpawn.Length, enemyIndex);
            positionIndex = indexRandom.GetNewIndex(enemySpawnPoints.Length, positionIndex);

            var g = Instantiate(enemiesToSpawn[enemyIndex], enemySpawnPoints[positionIndex].transform.position, Quaternion.identity);

            g.GetComponent<Enemy>().SetOwner(owner);
        }
    }

    public void EndSpawning()
    {

    }
}
