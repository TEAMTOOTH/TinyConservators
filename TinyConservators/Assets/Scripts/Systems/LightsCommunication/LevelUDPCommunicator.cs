using System;
using UnityEngine;

public class LevelUDPCommunicator : MonoBehaviour
{
    [SerializeField] bool sendTestMessage;

    [SerializeField] UDPManager udpManager;

    int controllerIndex;
    bool hasReceivedInitializionMessage = false;

    void OnEnable()
    {
        udpManager = GameObject.FindGameObjectWithTag("UDPCommunicator").GetComponent<UDPManager>();

        if (udpManager != null && udpManager.OnUPDMessageReceived != null)
        {
            udpManager.OnUPDMessageReceived.AddListener(HandleInitializationMessage);
        }
    }

    public void SendMessage(string message)
    {
        if (udpManager != null)
        {
            udpManager.SendMessage($"level,{message}");
        }
        else
        {
            Debug.LogError("No UDPManager found on this GameObject!");
        }
    }

    // This method can be assigned in the Inspector to a UnityEvent
    public void HandleInitializationMessage(string message)
    {
        
    }


    private void Update()
    {
        if (sendTestMessage == true)
        {
            SendMessage("level");
            sendTestMessage = false;
        }
    }

}

