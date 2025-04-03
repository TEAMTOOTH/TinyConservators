using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawnManager : MonoBehaviour
{
    [SerializeField] GameObject[] spawnPoints;
    int amountOfPlayersJoined;
    

    private void OnPlayerJoined(PlayerInput playerInput)
    {
        //(playerInput);
        SetUpPlayer(playerInput.gameObject, amountOfPlayersJoined);
        amountOfPlayersJoined++;
    }

    void SetUpPlayer(GameObject player, int id)
    {
        player.GetComponent<Player>().Initialize(id);
        player.transform.position = spawnPoints[id].transform.position;
    }
}
