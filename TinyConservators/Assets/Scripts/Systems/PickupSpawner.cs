using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [SerializeField] GameObject spawnObject;
    [SerializeField] int minimumAmount;
    [SerializeField] int maximumAmount;

    [SerializeField] float throwOutForce;

    public void SpawnPickups()
    {
        //Debug.Log("SpawningPickups");
        int amount = Random.Range(minimumAmount, maximumAmount);
        Debug.Log("SpawningPickups: " + amount);
        for (int i = 0; i < amount; i++)
        {
            GameObject spawnedObject = Instantiate(spawnObject, transform.position, Quaternion.identity);

            Rigidbody2D rb = spawnedObject.GetComponent<Rigidbody2D>();

            float arcAngle = 360f;

            // Random angle from 0 to 360 degrees
            float randomAngle = UnityEngine.Random.Range(0f, arcAngle);

            // Convert angle to a direction vector (rotate Vector2.up)
            Vector2 direction = Quaternion.Euler(0, 0, randomAngle) * Vector2.up;

            // Apply force (adjust throwOffForce to suit your needs)
            rb.AddForce(direction * throwOutForce, ForceMode2D.Impulse);
        }

        
    }
}
