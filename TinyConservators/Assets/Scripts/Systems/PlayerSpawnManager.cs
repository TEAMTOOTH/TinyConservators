using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using UnityEngine.InputSystem.Users;

public class PlayerSpawnManager : MonoBehaviour
{
    [SerializeField] GameObject[] spawnPoints;
    [SerializeField] GameObject[] sleepingPlayers;
    [SerializeField] float wakeUpTime;
    [SerializeField] bool inGame;
    [SerializeField] Vector2 initialSpawnForceFromTo;
    PlayerManager pm;

    private void Start()
    {
        pm = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<PlayerManager>();
        GameObject.FindGameObjectWithTag("DontDestroyManager").GetComponent<DontDestroyOnLoadManager>().AddDontDestroyObject(gameObject);
        //FindSpawnPoints();
        //Debug.Log("Player spawn manager is in start");
    }

    private void OnEnable()
    {
        FindSpawnPoints();
    }

    private void OnDisable()
    {
        
    }

    public void FindSpawnPoints() 
    {
        /*spawnPoints = GameObject
        .FindGameObjectWithTag("SpawnPoints")
        .GetComponentsInChildren<Transform>()
        .Skip(1) // Optional: skip the parent object if needed
        .Select(t => t.gameObject)
        .ToArray();*/
        //spawnPoints = GameObject.FindGameObjectWithTag("SpawnPoints").GetComponentsInChildren<Transform>();


        Debug.Log("Spawnpoint count is: " + spawnPoints.Length);
        
        //removing the first index as that is the parent
        //for(int i = 1; i < spawnPointContainer.Length; i++)
    }

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        //(playerInput);
        //Debug.Log(playerInput.gameObject);
        //Debug.Break();
        Debug.Log(pm.GetAmountOfPlayers());
        SetUpPlayer(playerInput.gameObject, pm.GetAmountOfPlayers());

        // play player join SFX
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/playerJoin");

    }

    public void SetInGame(bool inGame)
    {
        this.inGame = inGame;
    }

    void SetUpPlayer(GameObject player, int id)
    {
        Player p = player.GetComponent<Player>();
        if(p != null)
        {

            if (!inGame)
            {
                StartCoroutine(wakeUpPlayer());
                IEnumerator wakeUpPlayer()
                {
                    p.Initialize(id);

                    p.ShowVisual(true);
                    p.FullFreeze(false);
                    p.SetMoveState(false);
                    Vector2 spawnForce = new Vector2(Random.Range(initialSpawnForceFromTo.x, initialSpawnForceFromTo.y), 0);

                    if (id % 2 == 0)
                    {
                        p.transform.position = spawnPoints[1].transform.position;
                        //player.GetComponent<Rigidbody2D>().AddForce(spawnForce);
                        player.GetComponent<Rigidbody2D>().linearVelocity = spawnForce;
                    }
                    else
                    {
                        p.transform.position = spawnPoints[0].transform.position;
                        //player.GetComponent<Rigidbody2D>().AddForce(spawnForce * -1);
                        player.GetComponent<Rigidbody2D>().linearVelocity = spawnForce * -1;
                    }

                    //Debug.Break();
                    
                    //p.FullFreeze(true);
                    //p.ShowVisual(false);
                    pm.OnPlayerJoined(player.GetComponent<Player>());

                    //sleepingPlayers[id].GetComponent<Animator>().Play("WakeUp");
                    //yield return new WaitForSeconds(wakeUpTime);

                    yield return new WaitForSeconds(0f);
                    
                    //sleepingPlayers[id].SetActive(false);

                    





                }
            }
            else
            {

                p.Initialize(id);
                p.transform.position = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
                p.FullFreeze(false);
                pm.OnPlayerJoined(player.GetComponent<Player>());
                player.GetComponent<Player>().PlayPoof();
            }
            
        }
        else
        {
            Debug.Log("P is null");
        }
        
    }

    void WakeUpConservator()
    {

    }

    public void RemovePlayer(PlayerInput playerInputToRemove )
    {
        foreach (var device in playerInputToRemove.devices)
        {
            //InputUser.UnpairDevice(device);
        }

        // Destroy the PlayerInput component
        Destroy(playerInputToRemove);

        // Optionally, destroy the entire player game object
        Destroy(playerInputToRemove.gameObject);
    }
}
