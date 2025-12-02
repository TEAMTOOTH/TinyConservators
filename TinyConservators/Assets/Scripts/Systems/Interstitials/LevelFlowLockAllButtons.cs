using UnityEngine;

public class LevelFlowLockAllButtons : MonoBehaviour, ILevelFlowComponent
{
    [SerializeField] GameObject udpCommunicator;
    LevelFlowManager owner;
    public void FinishSection()
    {
        owner.ProgressFlow();
    }

    public void StartSection(LevelFlowManager flowManager)
    {
        owner = flowManager;
        udpCommunicator.GetComponent<CharacterCreatorUDPCaller>().LockAllButtons();
        FinishSection();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
