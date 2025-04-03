using UnityEngine;


public class Fruit : MonoBehaviour, IEatable
{
    [SerializeField] int pointsValue;
    public void Eat(GameObject eater)
    {
        
        FruitSpawner spawner = GetComponentInParent<FruitSpawner>();

        PointsReceiver pointsReceiver = eater.GetComponent<PointsReceiver>();

        if(pointsReceiver != null)
        {
            pointsReceiver.AddPoints(pointsValue);
        }

        if(spawner != null)
        {
            spawner.Respawn();
        }
        Destroy(gameObject);
    }
}
