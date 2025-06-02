using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [SerializeField] GameObject spawnObject;
    [SerializeField] int minimumAmount;
    [SerializeField] int maximumAmount;

    [SerializeField] float throwOutForce;

    /// <summary>
    /// Just spawn color without damage tie ins.
    /// </summary>
    /// <returns></returns>
    public GameObject[] SpawnPickups()
    {
        //Debug.Log("SpawningPickups");
        int amount = Random.Range(minimumAmount, maximumAmount);
        List<GameObject> spawnedObjects = new List<GameObject>();
        //Debug.Log("SpawningPickups: " + amount);
        for (int i = 0; i < amount; i++)
        {
            GameObject spawnedObject = Instantiate(spawnObject, transform.position, Quaternion.identity);
            spawnedObjects.Add(spawnedObject);
            
            Rigidbody2D rb = spawnedObject.GetComponent<Rigidbody2D>();

            float arcAngle = 360f;

            // Random angle from 0 to 360 degrees
            float randomAngle = UnityEngine.Random.Range(0f, arcAngle);

            // Convert angle to a direction vector (rotate Vector2.up)
            Vector2 direction = Quaternion.Euler(0, 0, randomAngle) * Vector2.up;

            // Apply force (adjust throwOffForce to suit your needs)
            rb.AddForce(direction * throwOutForce, ForceMode2D.Impulse);
        }

        return spawnedObjects.ToArray();
    }

    /// <summary>
    /// Spawn color with one damage tie in
    /// </summary>
    /// <param name="attackPoint"></param>
    /// <returns></returns>
    public GameObject[] SpawnPickups(GameObject fixObject)
    {
        
        //Debug.Log("SpawningPickups");
        if(minimumAmount < 2)
        {
            Debug.LogError("OVERRIDING USER INPUT to TWO as minimum. If you are using this one, you need more than two color dots.");
            minimumAmount = 2;
        }

        int amount = Random.Range(minimumAmount, maximumAmount);
        List<GameObject> spawnedObjects = new List<GameObject>();
        //Debug.Log("SpawningPickups: " + amount);
        

        for (int i = 0; i < amount; i++)
        {
            GameObject spawnedObject = Instantiate(spawnObject, transform.position, Quaternion.identity);
            spawnedObjects.Add(spawnedObject);

            if (i < amount / 2)
            {
                spawnedObject.GetComponent<IFixer>().SetOwner(fixObject);
            }

            Rigidbody2D rb = spawnedObject.GetComponent<Rigidbody2D>();

            float arcAngle = 360f;

            // Random angle from 0 to 360 degrees
            float randomAngle = UnityEngine.Random.Range(0f, arcAngle);

            // Convert angle to a direction vector (rotate Vector2.up)
            Vector2 direction = Quaternion.Euler(0, 0, randomAngle) * Vector2.up;

            // Apply force (adjust throwOffForce to suit your needs)
            rb.AddForce(direction * throwOutForce, ForceMode2D.Impulse);
        }

        return spawnedObjects.ToArray();
    }

    /// <summary>
    /// Spawn color with one damage tie in
    /// </summary>
    /// <param name="attackPoints"></param>
    /// <returns></returns>
    public GameObject[] SpawnPickups(List<GameObject> fixObjects)
    {
        if (minimumAmount < 2)
        {
            Debug.LogError("OVERRIDING USER INPUT to TWO as minimum. If you are using this one, you need more than two color dots.");
            minimumAmount = 2;
        }

        int amount = Random.Range(minimumAmount, maximumAmount);
        List<GameObject> spawnedObjects = new List<GameObject>();
        //Debug.Log("SpawningPickups: " + amount);
        for (int i = 0; i < amount; i++)
        {
            GameObject spawnedObject = Instantiate(spawnObject, transform.position, Quaternion.identity);
            spawnedObjects.Add(spawnedObject);
            
            if(i < amount / 2)
            {
                spawnedObject.GetComponent<IFixer>().SetOwner(fixObjects[0]);
            }
            else
            {
                spawnedObject.GetComponent<IFixer>().SetOwner(fixObjects[1]);
            }

            Rigidbody2D rb = spawnedObject.GetComponent<Rigidbody2D>();

            float arcAngle = 360f;

            // Random angle from 0 to 360 degrees
            float randomAngle = UnityEngine.Random.Range(0f, arcAngle);

            // Convert angle to a direction vector (rotate Vector2.up)
            Vector2 direction = Quaternion.Euler(0, 0, randomAngle) * Vector2.up;

            // Apply force (adjust throwOffForce to suit your needs)
            rb.AddForce(direction * throwOutForce, ForceMode2D.Impulse);
        }

        return spawnedObjects.ToArray();
    }
}
