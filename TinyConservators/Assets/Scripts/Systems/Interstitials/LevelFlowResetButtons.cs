using UnityEngine;

public class LevelFlowResetButtons : MonoBehaviour, ILevelFlowComponent
{
    [SerializeField] GameObject udpCommunicator;
    LevelFlowManager owner;
    public void FinishSection()
    {
        owner.ProgressFlow();
    }

    public void StartSection(LevelFlowManager flowManager)
    {
        udpCommunicator.GetComponent<CharacterCreatorUDPCaller>().UnlockButtons();
        FinishSection();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        udpCommunicator.GetComponent<CharacterCreatorUDPCaller>().LockAllButtons();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
