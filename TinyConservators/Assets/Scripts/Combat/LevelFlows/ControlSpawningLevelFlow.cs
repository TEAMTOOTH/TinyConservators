using UnityEngine;
using UnityEngine.InputSystem;

public class ControlSpawningLevelFlow : MonoBehaviour, ILevelFlowComponent
{
    [SerializeField] bool allowJoining;
    [SerializeField] GameObject joinManager; 
    
    PlayerInputManager pIM;
    PlayerSpawnManager pSM;


    LevelFlowManager owner;
    public void FinishSection()
    {
        throw new System.NotImplementedException();
    }

    public void StartSection(LevelFlowManager flowManager)
    {
        owner = flowManager;
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
        
    }
}
