using System.Collections.Generic;
using UnityEngine;

public class BossItemManager : MonoBehaviour
{
    [SerializeField] GameObject objectToSpawn;
    
    List<GameObject> spawnedObjects;

    public void SpawnObjects(int amount, float speed, float size)
    {
        spawnedObjects = new List<GameObject>();
        int num = 32; //Throwing this in here for now, can make it more complicated if we feel like it.

        for(int i = 0; i < amount; i++)
        {
            GameObject spawned = Instantiate(objectToSpawn, transform);
            int startIndex = num / amount * i;

            spawned.GetComponent<ClockMovement>().InitializeMoving(speed, startIndex, num, size);
            spawnedObjects.Add(spawned);
        }
    }

    public void DespawnObjects()
    {
        foreach(GameObject item in spawnedObjects)
        {
            Destroy(item);
        }

        spawnedObjects = new List<GameObject>();
    }
}
