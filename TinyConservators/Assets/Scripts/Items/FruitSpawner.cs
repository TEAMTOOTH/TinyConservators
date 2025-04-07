using UnityEngine;
using System.Collections;

public class FruitSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] spawnObjects;
    [SerializeField] float spawnTime;

    Transform[] spawnPoints;
    bool canSpawn = true;

    int previousSpawnIndex = 100; //Set to a high number so that get unique random only runs once on the first go.

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        var transforms = GetComponentsInChildren<Transform>();

        spawnPoints = new Transform[transforms.Length - 1];

        for(int i = 0; i < spawnPoints.Length; i++)
        {
            spawnPoints[i] = transforms[i + 1];
        }
        

        //SpawnFruit();
    }

    public void TestStartMethod()
    {
        Invoke("StartSpawning", 3);
        Invoke("StopSpawning", 63);
        //Invoke("StopSpawning", 15);
        
    }

    void SpawnFruit()
    {
        if (canSpawn)
        {
            int spawnIndex = Random.Range(0, spawnPoints.Length);
           
            //Spawning directly as child, gave weird effects. Changed so that the spawning goes smoother.
            GameObject g = Instantiate(spawnObjects[Random.Range(0, spawnObjects.Length)], spawnPoints[spawnIndex].position, Quaternion.identity);

            g.transform.parent = spawnPoints[spawnIndex];
            previousSpawnIndex = spawnIndex;
        }
    }

    public void Respawn()
    {
        StartCoroutine(WaitAndSpawn());
        IEnumerator WaitAndSpawn()
        {
            yield return new WaitForSeconds(spawnTime);
            SpawnFruit();
        }
    }

    void StopSpawning()
    {
        canSpawn = false;
        GameManager gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        gm.DetermineWinner();
    }

    int GetUniqueRandom()
    {
        return 0;
    }

    //Refactor this later, done for a test on the 07.04
    void StartSpawning()
    {
        SpawnFruit();
    }

    
}
