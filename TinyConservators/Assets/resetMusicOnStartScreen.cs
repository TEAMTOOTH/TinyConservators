using UnityEngine;

public class resetMusicOnStartScreen : MonoBehaviour
{
    //private FMOD.Studio.EventInstance musicSelector; //saving code bits here in case I need it later

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/music/stopMusic"); // this is a command event that stops the main music
        //musicSelector = FMODUnity.RuntimeManager.CreateInstance("event:/music/discoSoulLoop"); //saving this
        //musicSelector.start(); //saving this
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
