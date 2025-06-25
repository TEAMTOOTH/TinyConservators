using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    int amountOfPlayersJoined;
    List<Player> players = new List<Player>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlayerJoined(Player p)
    {
        players.Add(p);
    }

    public void OnPlayerUnjoined(Player p)
    {
        players.Remove(p);
    }

    public int GetAmountOfPlayers()
    {
        return players.Count;
    }
}
