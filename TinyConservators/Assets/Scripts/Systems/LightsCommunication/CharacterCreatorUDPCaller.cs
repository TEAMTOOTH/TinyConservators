using UnityEngine;

public class CharacterCreatorUDPCaller : MonoBehaviour
{

    LevelUDPCommunicator levelUDP;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        levelUDP = GetComponent<LevelUDPCommunicator>();
    }
}
