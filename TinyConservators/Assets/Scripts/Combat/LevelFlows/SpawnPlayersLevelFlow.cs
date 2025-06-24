using System;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class SpawnPlayersLevelFlow : MonoBehaviour, ILevelFlowComponent
{
    [SerializeField] SpawnPoint[] spawnPoints;
    [SerializeField] bool visualSpawn;
    LevelFlowManager owner;

    public void FinishSection()
    {
        owner.ProgressFlow();
    }



    public void StartSection(LevelFlowManager flowManager)
    {
        owner = flowManager;

        //spawnPoints
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        //FreezePlayers(true, players);
        SpawnPlayers(players);
        FinishSection();
       
    }

    private void FreezePlayers(bool state, GameObject[] players)
    {
        for (int i = 0; i < players.Length; i++)
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

    void SpawnPlayers(GameObject[] players)
    {
        for (int i = 0; i < players.Length; i++)
        {
            if (i < players.Length / 2)
            {
                players[i].transform.localPosition = spawnPoints[0].transform.position;
                if(visualSpawn)
                    spawnPoints[0].GetComponent<SpawnPoint>().Spawn(players[i]);
            }
            else
            {
                players[i].transform.localPosition = spawnPoints[1].transform.position;
                if (visualSpawn)
                    spawnPoints[1].GetComponent<SpawnPoint>().Spawn(players[i]);
            }
            
        }
    }
}
