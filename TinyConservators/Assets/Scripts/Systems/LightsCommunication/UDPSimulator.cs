using UnityEngine;

public class UDPSimulator : MonoBehaviour
{
    [SerializeField] bool sendMessage;

    [SerializeField] UDPManager invokerScript;

    void OnEnable()
    {
        invokerScript = GameObject.FindGameObjectWithTag("UDPCommunicator").GetComponent<UDPManager>();
        Debug.Log(invokerScript.gameObject.name);
        if (invokerScript != null && invokerScript.OnUPDMessageReceived != null)
        {
            invokerScript.OnUPDMessageReceived.AddListener(HandleMessage);
        }
    }


    [ContextMenu("Send Test UDP Message")]
    public void SendTestMessage()
    {
        UDPManager udpManager = GetComponent<UDPManager>();
        if (udpManager != null)
        {
            udpManager.SendMessage("Hello UDP!");
        }
        else
        {
            Debug.LogError("No UDPManager found on this GameObject!");
        }
    }

    // This method can be assigned in the Inspector to a UnityEvent
    public void HandleMessage(string message)
    {
        Debug.Log("Message received: " + message);
    }
    


    private void Update()
    {
        if(sendMessage == true)
        {
            SendTestMessage();
            sendMessage = false;
        }
    }
}