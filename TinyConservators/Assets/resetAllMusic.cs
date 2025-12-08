using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class resetAllMusic : MonoBehaviour, ILevelFlowComponent
{
    LevelFlowManager owner;
    private const string MasterBusPath = "bus:/";
    private Bus masterBus;

    public void FinishSection()
    {
        owner.ProgressFlow();
    }

    public void StartSection(LevelFlowManager flowManager)
    {
        owner = flowManager;

        FMODUnity.RuntimeManager.PlayOneShot("event:/music/stopMusic"); // this is a command event that stops all music events within FMOD


        // Get the FMOD Studio system
        FMOD.Studio.System studioSystem = RuntimeManager.StudioSystem;

        // Get all loaded banks
        studioSystem.getBankList(out Bank[] banks);

        foreach (Bank bank in banks)
        {
            // Get all event descriptions within each bank
            bank.getEventList(out EventDescription[] eventDescriptions);

            foreach (EventDescription eventDescription in eventDescriptions)
            {
                // Release all active instances of this event description
                eventDescription.releaseAllInstances();
            }
        }

        Debug.Log("All FMOD event instances released.");


        //Turn off the boss voice layer
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("bossVoice", 1);

        // Reset boss getting hit
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("bossGotHit", 0);

        // hard code stop all sounds


        masterBus.stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);


        FinishSection();
    }

    
}