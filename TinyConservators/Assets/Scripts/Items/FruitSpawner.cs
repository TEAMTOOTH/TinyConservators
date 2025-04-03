using UnityEngine;
using System.Collections;

public class FruitSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] spawnObjects;
    [SerializeField] float spawnTime;

    Transform[] spawnPoints;
    bool canSpawn = true;
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
        Invoke("StopSpawning", 63);
        SpawnFruit();
    }

    void SpawnFruit()
    {
        if (canSpawn)
        {
            int spawnIndex = Random.Range(0, spawnPoints.Length);
           
            //Spawning directly as child, gave weird effects. Changed so that the spawning goes smoother.
            GameObject g = Instantiate(spawnObjects[Random.Range(0, spawnObjects.Length)], spawnPoints[spawnIndex].position, Quaternion.identity);

            g.transform.parent = spawnPoints[spawnIndex];
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
    }

    
}
