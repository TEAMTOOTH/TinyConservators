using UnityEngine;

public class CharacterCreatorUDPCaller : MonoBehaviour
{

    LevelUDPCommunicator levelUDP;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        levelUDP = GetComponent<LevelUDPCommunicator>();
    }

    public void UnlockButtons()
    {
       
        string[] playerNames = { "blue", "red", "purple", "green", "orange", "table" };

        for(int i = 0; i < playerNames.Length; i++)
        {
            levelUDP.SendMessageOverridePrefix($"{playerNames[i]},reset");
        }
    }

    public void LockAllButtons()
    {
        levelUDP.SendMessageOverridePrefix($"lockbuttons");
    }
}
