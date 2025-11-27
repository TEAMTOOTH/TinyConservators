using UnityEngine;

public class bossVoiceFade : MonoBehaviour, ILevelFlowComponent
{
    LevelFlowManager owner;

    public void FinishSection()
    {

        //Turn off the boss voice layer
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("bossVoice", 0);

        owner.ProgressFlow();
    }

    public void StartSection(LevelFlowManager flowManager)
    {
        owner = flowManager;

        // Prep to slowly mute boss voice
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("bossGotHit", 0);

        FinishSection();
    }
}
