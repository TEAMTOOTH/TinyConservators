using UnityEngine;

public class LevelFlowLockAllButtons : MonoBehaviour, ILevelFlowComponent
{
    LevelUDPCommunicator udpCommunicator;
    LevelFlowManager owner;

    void Awake()
    {
        udpCommunicator = GameObject.FindGameObjectWithTag("LevelUDPManager")?.GetComponent<LevelUDPCommunicator>();
    }

    public void FinishSection()
    {
        owner.ProgressFlow();
    }

    public void StartSection(LevelFlowManager flowManager)
    {
        owner = flowManager;
        udpCommunicator.GetComponent<LevelUDPCommunicator>().SendMessageOverridePrefix("lockbuttons");
        FinishSection();
    }

    void LockAllButtons()
    {

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
