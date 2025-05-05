using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Level : MonoBehaviour
{
    [SerializeField] float levelLength;

    IInterstitial interstitial;
    public UnityEvent onStart;
    public UnityEvent onEnd;
    
    SpawnPoint[] spawnPoints;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    IEnumerator Start()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        FreezePlayers(true, players);

        interstitial = GameObject.FindGameObjectWithTag("Interstitial").GetComponent<IInterstitial>(); //Rememeber this only finds one of them.
        spawnPoints = GetComponentsInChildren<SpawnPoint>();
        PutPlayersIntoPosition(players);

        interstitial.StartInterstitial();

        yield return new WaitForSeconds(interstitial.GetLength());

        interstitial.EndInterstitial();
        onStart.Invoke();

        FreezePlayers(false, players);
        yield return new WaitForSeconds(levelLength);
        onEnd.Invoke();
        

    }

    public SpawnPoint[] GetSpawnPoints()
    {
        return spawnPoints;
    }

    void FreezePlayers(bool state, GameObject[] players)
    {
        for(int i = 0; i < players.Length; i++)
        {
            if (state)
            {
                players[i].GetComponent<Player>().State = PlayerStates.paused;
            }
            else
            {
                players[i].GetComponent<Player>().State = PlayerStates.moving;
            }
            
        }
    }

    void PutPlayersIntoPosition(GameObject[] players)
    {
        for(int i = 0; i < players.Length; i++)
        {
            players[i].transform.position = spawnPoints[i].transform.position;
        }
    } 
}
