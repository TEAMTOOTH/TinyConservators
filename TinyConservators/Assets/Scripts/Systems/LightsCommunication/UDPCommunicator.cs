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
    UdpClient udpClient;
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

        //InvokeRepeating("SendUDPSignal", 1, 3);
        //SendTCPSignal();
        path = Application.dataPath + Path.AltDirectorySeparatorChar + "/" + pathName;
        persistentPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + pathName;
        string json;
        IPSave data;
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




        udpClient = new UdpClient();
        //ipEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 26950);

        //Debug.Log("in awake udp");
        //10.101.60.201
        ipEndPoint = new IPEndPoint(IPAddress.Parse(data.iPAdress), 4703);
        //PlayerPrefs.Save();
        //ipEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 4703);
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

        udpClient.Send(buffer, buffer.Length, ipEndPoint);
    }

}