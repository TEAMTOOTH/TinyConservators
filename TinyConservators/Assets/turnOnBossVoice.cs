using UnityEngine;

public class turnOnBossVoice : MonoBehaviour, ILevelFlowComponent
{
    LevelFlowManager owner;

    public void FinishSection()
    {
        owner.ProgressFlow();
    }

    public void StartSection(LevelFlowManager flowManager)
    {
        owner = flowManager;

        //Turn off the boss voice layer
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("bossVoice", 1);

        // Reset boss getting hit
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("bossGotHit", 0);

        FinishSection();

    }
}
