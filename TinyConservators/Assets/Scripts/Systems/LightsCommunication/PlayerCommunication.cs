using System;
using UnityEngine;

public class PlayerCommunication : MonoBehaviour
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
            udpManager.SendMessage($"{controllerIndex},{message}");
        }
        else
        {
            Debug.LogError("No UDPManager found on this GameObject!");
        }
    }

    // This method can be assigned in the Inspector to a UnityEvent
    public void HandleInitializationMessage(string message)
    {
        if (!hasReceivedInitializionMessage)
        {
            controllerIndex = (int)Enum.Parse(typeof(playerControllers), message);
            GetComponentInChildren<CharacterCustomizer>()?.Initialize(controllerIndex);
            hasReceivedInitializionMessage = true;
            Debug.Log("Initializing player with color: " + message + " and index: " + controllerIndex);
        }
    }


    private void Update()
    {
        if(sendTestMessage == true)
        {
            SendMessage("player");
            sendTestMessage = false;
        }
    }

}

public enum playerControllers
{
    blue,
    purple,
    cyan,
    red,
    green,
    yellow
}
