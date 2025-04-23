using System.Collections;
using UnityEngine;

public class Level : MonoBehaviour
{
    SpawnPoint[] spawnPoints;
    IInterstitial interstitial;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    IEnumerator Start()
    {
        spawnPoints = GetComponentsInChildren<SpawnPoint>();
        interstitial.StartInterstitial();

        yield return new WaitForSeconds(interstitial.GetLength());

        interstitial.EndInterstitial();
        //Start level or countdown or smth.
        
    }

    public SpawnPoint[] GetSpawnPoints()
    {
        return spawnPoints;
    }
}
