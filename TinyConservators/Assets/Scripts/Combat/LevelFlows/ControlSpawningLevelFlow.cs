using UnityEngine;
using UnityEngine.InputSystem;

public class ControlSpawningLevelFlow : MonoBehaviour, ILevelFlowComponent
{
    [SerializeField] bool allowJoining;
    GameObject joinManager; 
    
    PlayerInputManager pIM;
    PlayerSpawnManager pSM;


    LevelFlowManager owner;

    void Start()
    {
        joinManager = GameObject.FindGameObjectWithTag("PlayerJoinManager");
    }

    public void FinishSection()
    {
        owner.ProgressFlow();
    }

    public void StartSection(LevelFlowManager flowManager)
    {
        owner = flowManager;
        
        if (joinManager != null)
        {
            pIM = joinManager.GetComponent<PlayerInputManager>();
            pSM = joinManager.GetComponent<PlayerSpawnManager>();

            if (allowJoining)
            {
                pIM.EnableJoining();
                pSM.FindSpawnPoints();
            }
            else
            {
                pIM.DisableJoining();
            }

            pSM.SetInGame(true); // A bit dirty, but as far as I know it won't really affect anything later on, so why make it complicated.
        }
        else
        {
            Debug.LogWarning("Could not find join manager! Skipping the join logic");
        }
        
        FinishSection();
    }
}
