using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawnManager : MonoBehaviour
{
    [SerializeField] GameObject[] spawnPoints;
    [SerializeField] GameObject[] sleepingPlayers;
    [SerializeField] float wakeUpTime;
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
            StartCoroutine(wakeUpPlayer());
            IEnumerator wakeUpPlayer()
            {
                p.Initialize(id);
                p.transform.position = spawnPoints[id].transform.position;
                p.FullFreeze(true);
                p.ShowVisual(false);
                sleepingPlayers[id].GetComponent<Animator>().Play("WakeUp");
                yield return new WaitForSeconds(wakeUpTime);
                sleepingPlayers[id].SetActive(false);
                p.ShowVisual(true);
                p.FullFreeze(false);

            }
            amountOfPlayersJoined++;
        }
        else
        {
            Debug.Log("P is null");
        }
        
    }

    void WakeUpConservator()
    {

    }
}
