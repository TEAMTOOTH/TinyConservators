using System;
using System.Collections;
using UnityEngine;

public class PlayerCommunication : MonoBehaviour
{
    [SerializeField] bool sendTestMessage;

    [SerializeField] UDPManager udpManager;

    string controllerColor;
    bool hasReceivedInitializionMessage = false;

    void OnEnable()
    {
        udpManager = GameObject.FindGameObjectWithTag("UDPCommunicator")?.GetComponent<UDPManager>();
        
        if (udpManager != null && udpManager.OnUPDMessageReceived != null)
        {
            udpManager.OnUPDMessageReceived.AddListener(HandleInitializationMessage);
        }

        StartCoroutine(GiveRandomColor());

        IEnumerator GiveRandomColor()
        {
            yield return new WaitForSeconds(.5f);
            playerControllers[] values = (playerControllers[])System.Enum.GetValues(typeof(playerControllers));
            int index = UnityEngine.Random.Range(0, values.Length);

            ChooseBody(values[index].ToString());
        }
        
    }

    public void SendMessage(string message)
    {
        if (udpManager != null)
        {
            udpManager.SendMessage($"{controllerColor},{message}");
        }
        else
        {
            Debug.LogError("No UDPManager found on this GameObject!");
        }
    }

    // This method can be assigned in the Inspector to a UnityEvent
    public void HandleInitializationMessage(string message)
    {
        Debug.Log("HandleInitializationMessage received: " + message);

        if (!Enum.IsDefined(typeof(playerControllers), message))
            return;
        
        if (!hasReceivedInitializionMessage)
        {
            ChooseBody(message);
            hasReceivedInitializionMessage = true;
        }
    }

    public void ChooseBody(string message)
    {
        StopAllCoroutines();
        int controllerIndex;
        controllerIndex = (int)Enum.Parse(typeof(playerControllers), message);
        controllerColor = message;
        GetComponentInChildren<CharacterCustomizer>()?.Initialize(controllerIndex);
        
        Debug.Log("Initializing player with color: " + message + " and index: " + controllerIndex);
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
    green,
    purple,
    red,
    table,
    yellow
}
