using UnityEngine;

public class SendMessageToAllConservatorsLevelFlow : MonoBehaviour, ILevelFlowComponent
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

        string[] playerNames = { "blue", "red", "purple", "green", "yellow", "table" };

        for (int i = 0; i < playerNames.Length; i++)
        {
            udpCommunicator.SendMessageOverridePrefix($"{playerNames[i]},{message}");
        }

        FinishSection();
    }
}
