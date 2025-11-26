using UnityEngine;

public class bossShutup : MonoBehaviour, ILevelFlowComponent
{
    LevelFlowManager owner;

    public void FinishSection()
    {
        owner.ProgressFlow();
    }

    public void StartSection(LevelFlowManager flowManager)
    {
        owner = flowManager;

        // Prep to sharply mute boss voice
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("bossGotHit", 1);

        //Turn off the boss voice layer
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("bossVoice", 0);
        
        FinishSection();
    }
}
