using UnityEngine;


public class Fruit : MonoBehaviour, IEatable
{
    public void Eat(GameObject eater)
    {
        Debug.Log(eater.name);
        Destroy(gameObject);
    }
}
