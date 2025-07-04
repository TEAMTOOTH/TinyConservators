using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawnFlowManager : MonoBehaviour
{

    PlayerSpawnManager psm;
    JoinManagerInGame jMIG;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject.FindGameObjectWithTag("DontDestroyManager").GetComponent<DontDestroyOnLoadManager>().AddDontDestroyObject(gameObject);
        psm = GetComponent<PlayerSpawnManager>();
        jMIG = GetComponent<JoinManagerInGame>();
    }

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        //(playerInput);
        //Debug.Log(playerInput.gameObject);
        //Debug.Break();
        //SetUpPlayer(playerInput.gameObject, pm.GetAmountOfPlayers());

    }
}
