using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Text;
using UnityEngine;
using System.Linq;
using System;
using System.IO;

public class UDPCommunicator : MonoBehaviour
{
    UdpClient udpSender;
    UdpClient udpReceiver;
    IPEndPoint ipEndPoint;

    string pathName = "Config.json";
    string path = "";
    string persistentPath = "";

    // This is really the only blurb of code you need to implement a Unity singleton
    private static UDPCommunicator _Instance;
    public static UDPCommunicator Instance
    {
        get
        {
            if (!_Instance)
            {
                _Instance = new GameObject().AddComponent<UDPCommunicator>();
                // name it for easy recognition
                _Instance.name = _Instance.GetType().ToString();
                // mark root as DontDestroyOnLoad();
                DontDestroyOnLoad(_Instance.gameObject);
            }
            return _Instance;
        }
    }

    public void Awake()
    {
        Debug.Log("In awake UDP communicator");
        //InvokeRepeating("SendUDPSignal", 1, 3);
        //SendTCPSignal();
        path = Application.dataPath + Path.AltDirectorySeparatorChar + "/" + pathName;
        persistentPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + pathName;
        string json;
        IPSave data;

        Debug.Log(persistentPath);
        if (!File.Exists(persistentPath))
        {

            string newData = JsonUtility.ToJson(new IPSave("10.101.60.201"));
            Debug.Log($"Saving data at: {persistentPath}, data is: {newData}");

            using StreamWriter writer = new StreamWriter(persistentPath);
            writer.Write(newData);
        }

        using StreamReader reader = new StreamReader(persistentPath);

        json = reader.ReadToEnd();
        data = JsonUtility.FromJson<IPSave>(json);


        //Sender
        udpSender = new UdpClient();  // outbound only (does NOT bind)
        //ipEndPoint = new IPEndPoint(IPAddress.Parse(data.iPAdress), 4704);
        ipEndPoint = new IPEndPoint(IPAddress.Loopback, 4703);


        //Receiver
        int listenPort = 4703;        // same port as sending, or change if needed
        udpReceiver = new UdpClient(listenPort);   // bind here for incoming messages

        Debug.Log($"UDP: Sending to {data.iPAdress}:4703, Listening on port {listenPort}");

        //StartReceiving(4703);
        ReceiveLoop();
    }


    // Start is called before the first frame update
    void Start()
    {

    }


    bool HasNonASCIIChars(string str)
    {
        return (System.Text.Encoding.UTF8.GetByteCount(str) != str.Length);
    }

    /// <summary>
    /// Sends a UDP signal out. Will send invalidmessage if message is not valid ASCII.
    /// </summary>
    /// <param name="message"></param>
    public void SendUDPSignal(string message)
    {
        byte[] buffer;
        if (!HasNonASCIIChars(message))
        {
            buffer = Encoding.ASCII.GetBytes(message);
        }
        else
        {
            buffer = Encoding.ASCII.GetBytes("invalidmessage");
            Debug.LogError("Message has non ascii characters");
        }
        //Debug.Log();

        udpSender.Send(buffer, buffer.Length, ipEndPoint);
    }

    /*public void StartReceiving(int listenPort = 4703)
    {
        
        udpReceiver = new UdpClient(listenPort);
        ipEndPoint = new IPEndPoint(IPAddress.Any, listenPort);

        Debug.Log($"UDP Receiver started on port {listenPort}");
        ReceiveLoop();
    }*/

    private async void ReceiveLoop()
    {
        while (true)
        {
            UdpReceiveResult result = await udpReceiver.ReceiveAsync();
            string msg = Encoding.ASCII.GetString(result.Buffer);

            Debug.Log("Simulator → Received: " + msg);
        }
        /*
        while (true)
        {

            try
            {
                Debug.Log("Before awaiting result");
                UdpReceiveResult result = await udpReceiver.ReceiveAsync();

                string received = Encoding.ASCII.GetString(result.Buffer);

                Debug.Log(received);
                // Handle message on main thread
                UnityMainThreadDispatcher(received);
            }
            catch (ObjectDisposedException)
            {
                // Listener was closed intentionally
                Debug.Log("Listener was closed intentionally");
                break;
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                break;
            }
        }*/
    }

    private void UnityMainThreadDispatcher(string message)
    {
        // If you already use a dispatcher, plug it in here.
        // Otherwise use this simple method:
        UnityEngine.WSA.Application.InvokeOnAppThread(() =>
        {
            Debug.Log($"UDP RECEIVED: {message}");
            // TODO: Handle your message here
            //Send out an event with the color message or grab who joined last and apply color to them?
        }, false);
    }

}