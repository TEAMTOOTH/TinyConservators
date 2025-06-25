using UnityEngine;
using UnityEngine.InputSystem;

public class JoinManagerInGame : MonoBehaviour
{
    [SerializeField] GameObject[] spawnPoints;
    //[SerializeField] GameObject[] sleepingPlayers;
    [SerializeField] float wakeUpTime;
    int amountOfPlayersJoined;

    private void Start()
    {
        
    }

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        PlayerManager pm = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<PlayerManager>();
        Debug.Log("In player joined");
        //(playerInput);
        //Debug.Log(playerInput.gameObject);
        //Debug.Break();
        SetUpPlayer(playerInput.gameObject, pm.GetAmountOfPlayers());
    }

    void SetUpPlayer(GameObject player, int id)
    {
        Player p = player.GetComponent<Player>();
        if (p != null)
        {
            /*StartCoroutine(wakeUpPlayer());
            IEnumerator wakeUpPlayer()
            {
                
                p.FullFreeze(true);
                p.ShowVisual(false);
                sleepingPlayers[id].GetComponent<Animator>().Play("WakeUp");
                yield return new WaitForSeconds(wakeUpTime);
                sleepingPlayers[id].SetActive(false);
                p.ShowVisual(true);
                p.FullFreeze(false);

            }
            */
            p.Initialize(id);
            p.transform.position = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
            p.FullFreeze(false);
            //p.GetComponent<Animator>().Play("Poof");
            GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<PlayerManager>().OnPlayerJoined(player.GetComponent<Player>());

            //amountOfPlayersJoined++;
        }


       

    }

    void WakeUpConservator()
    {

    }
}
