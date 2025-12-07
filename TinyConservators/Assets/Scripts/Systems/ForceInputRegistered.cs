using UnityEngine;

public class ForceInputRegistered : MonoBehaviour
{
    Player[] players;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject[] p = GameObject.FindGameObjectsWithTag("Player");

        players = new Player[p.Length];

        for(int i = 0; i < players.Length; i++)
        {
            players[i] = p[i].GetComponent<Player>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].InputRegistered();
        }
    }
}
