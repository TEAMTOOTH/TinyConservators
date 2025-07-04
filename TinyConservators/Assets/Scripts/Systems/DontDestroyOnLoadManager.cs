using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoadManager : MonoBehaviour
{
    List<GameObject> dontDestroyOnLoadObjects;
    private void Awake()
    {
        dontDestroyOnLoadObjects = new List<GameObject>();
        DontDestroyOnLoad(gameObject); //Doing it here, so that it does not get added to the list and I can destroy it myself later.
    }
    public void AddDontDestroyObject(GameObject objectToAdd)
    {
        DontDestroyOnLoad(objectToAdd);
        dontDestroyOnLoadObjects.Add(objectToAdd);
    }

    public void DestroyAllDontDestroyObjects()
    {
        foreach (GameObject g in dontDestroyOnLoadObjects)
        {
            Destroy(g);
        }
        Destroy(gameObject);
    }
}
