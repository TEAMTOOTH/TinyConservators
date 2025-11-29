using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class UDPMessageUnityEvent : UnityEvent<string> { }

public class UDPManager : MonoBehaviour
{
    public string targetIP = "127.0.0.1";
    public int targetPort = 4703;
    public int listenPort = 4703;

    private UdpClient udpReceiver;
    private UdpClient udpSender;
    private IPEndPoint ipEndPoint;

    private int receivedCount = 0;
    private int sentCount = 0;


    public UDPMessageUnityEvent OnUPDMessageReceived;

    public void TriggerUDPMessageReceived(string message)
    {
        // Invoke the UnityEvent, passing the parameters
        OnUPDMessageReceived.Invoke(message);
    }

    private void Awake()
    {
        // Initialize sender
        udpSender = new UdpClient();
        ipEndPoint = new IPEndPoint(IPAddress.Parse(targetIP), targetPort);

        // Initialize receiver
        udpReceiver = new UdpClient(listenPort);
        Debug.Log($"[UDP] Receiver listening on port {listenPort}");

        // Start receive loop
        _ = ReceiveLoop();
    }


    


    private void Start()
    {
        GameObject.FindGameObjectWithTag("DontDestroyManager")?.GetComponent<DontDestroyOnLoadManager>().AddDontDestroyObject(gameObject);
    }

    private async Task ReceiveLoop()
    {
        try
        {
            while (true)
            {
                UdpReceiveResult result = await udpReceiver.ReceiveAsync();
                receivedCount++;

                string message = Encoding.UTF8.GetString(result.Buffer);

                Debug.Log(
                    $"[UDP RECEIVED #{receivedCount}] {DateTime.Now:HH:mm:ss.fff}\n" +
                    $"Message: {message}\n" +
                    $"From: {result.RemoteEndPoint}"
                );

                TriggerUDPMessageReceived(message);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"[UDP] ReceiveLoop Exception:\n{ex}");
        }
    }

    public void SendMessage(string message)
    {
        if (udpSender == null || ipEndPoint == null)
        {
            Debug.LogError("[UDP] Sender not initialized!");
            return;
        }

        byte[] data = Encoding.UTF8.GetBytes(message);
        udpSender.Send(data, data.Length, ipEndPoint);

        sentCount++;

        Debug.Log(
            $"[UDP SENT #{sentCount}] {DateTime.Now:HH:mm:ss.fff}\n" +
            $"Message: {message}\n" +
            $"To: {ipEndPoint}"
        );
    }

    private void OnApplicationQuit()
    {
        udpReceiver?.Close();
        udpSender?.Close();
    }
}
