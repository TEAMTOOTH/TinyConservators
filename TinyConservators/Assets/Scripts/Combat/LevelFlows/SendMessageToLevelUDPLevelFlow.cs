using UnityEngine;

public class SendMessageToLevelUDPLevelFlow : MonoBehaviour, ILevelFlowComponent
{
    [SerializeField] string message;
    LevelFlowManager owner;
    LevelUDPCommunicator udpCommunicator;


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

        udpCommunicator?.SendMessage(message);

        FinishSection();
    }
}

