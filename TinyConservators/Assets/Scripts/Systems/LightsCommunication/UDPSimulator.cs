using UnityEngine;

public class UDPSimulator : MonoBehaviour
{
    [SerializeField] bool sendMessage;

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

    private void Update()
    {
        if(sendMessage == true)
        {
            SendTestMessage();
            sendMessage = false;
        }
    }
}