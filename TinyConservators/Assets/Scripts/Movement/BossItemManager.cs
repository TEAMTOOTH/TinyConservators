using System.Collections.Generic;
using UnityEngine;

public class BossItemManager : MonoBehaviour
{
    [SerializeField] DamageObjectInformation damageObjectInformation;
    
    List<GameObject> spawnedObjects;

    private void Start()
    {
        spawnedObjects = new List<GameObject>();
    }

    public void SpawnObjects()
    {
        
        int num = 32; //Throwing this in here for now, can make it more complicated if we feel like it.

        for(int i = 0; i < damageObjectInformation.amount; i++)
        {
            GameObject spawned = Instantiate(damageObjectInformation.damageObject, transform);
            int startIndex = num / damageObjectInformation.amount * i;

            spawned.GetComponent<ClockMovement>().InitializeMoving(damageObjectInformation.revolutionTime, startIndex, num, damageObjectInformation.size);
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
