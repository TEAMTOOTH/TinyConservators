using System;
using UnityEngine;

public class PlayerJoinUDPManager : MonoBehaviour
{
    bool hasReceivedInitializionMessage;
    [SerializeField] UDPManager udpManager;
    //[SerializeField] PlayerInputManager playerInputManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        udpManager = GameObject.FindGameObjectWithTag("UDPCommunicator")?.GetComponent<UDPManager>();

        if (udpManager != null && udpManager.OnUPDMessageReceived != null)
        {
            udpManager.OnUPDMessageReceived.AddListener(HandleInitializationMessage);
        }
    }

    void HandleInitializationMessage(string message)
    {
        if (message.Split(',')[0].Length < 1)
            return;

        if (!Enum.IsDefined(typeof(playerControllers), message))
            return;


        if (!hasReceivedInitializionMessage)
        {
            //ChooseBody(message);

            hasReceivedInitializionMessage = true;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
