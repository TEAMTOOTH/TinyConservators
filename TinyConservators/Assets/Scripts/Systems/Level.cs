using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Level : MonoBehaviour
{
    [SerializeField] float levelLength;
    [SerializeField] bool spawnPlayersVisually;

    IInterstitial interstitial;
    public UnityEvent onStart;
    public UnityEvent onEnd;
    
    SpawnPoint[] spawnPoints;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    IEnumerator Start()
    {
        //GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        //FreezePlayers(true, players);

         
        //spawnPoints = GetComponentsInChildren<SpawnPoint>();
        

        //PutPlayersIntoPosition(players);

        //GameObject g = GameObject.FindGameObjectWithTag("Interstitial");

        yield return null;

        /*if (g != null)
        {
            interstitial = g.GetComponent<IInterstitial>(); //Rememeber this only finds one of them.
            interstitial.StartInterstitial();

            yield return new WaitForSeconds(interstitial.GetLength());
            interstitial.EndInterstitial();
        }*/

        //yield return new WaitForSeconds(3f);
        
        //onStart.Invoke();

        
        //yield return new WaitForSeconds(levelLength);
        //onEnd.Invoke();
        

    }

    public SpawnPoint[] GetSpawnPoints()
    {
        return spawnPoints;
    }

}
