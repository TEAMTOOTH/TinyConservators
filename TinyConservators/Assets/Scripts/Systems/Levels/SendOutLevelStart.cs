using UnityEngine;

public class SendOutLevelStart : MonoBehaviour
{
    [SerializeField] string startUdpMessage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SendOutUDPMessage(startUdpMessage);
    }

    public void SendOutUDPMessage(string udpMessage)
    {
        GetComponent<LevelUDPCommunicator>()?.SendMessage(udpMessage);
    }
}
