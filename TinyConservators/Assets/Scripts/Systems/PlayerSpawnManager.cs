using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawnManager : MonoBehaviour
{
    [SerializeField] GameObject[] spawnPoints;
    int amountOfPlayersJoined;
    

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        //(playerInput);
        //Debug.Log(playerInput.gameObject);
        //Debug.Break();
        SetUpPlayer(playerInput.gameObject, amountOfPlayersJoined);
        
    }

    void SetUpPlayer(GameObject player, int id)
    {
        Player p = player.GetComponent<Player>();
        if(p != null)
        {
            p.Initialize(id);
            p.transform.position = spawnPoints[id].transform.position;
            amountOfPlayersJoined++;
        }
        else
        {
            Debug.Log("P is null");
        }
        
    }
}
