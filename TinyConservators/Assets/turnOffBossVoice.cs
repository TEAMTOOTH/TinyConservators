using UnityEngine;

public class turnOffBossVoice : MonoBehaviour, ILevelFlowComponent
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
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("bossVoice", 0);
        FinishSection();
        
    }
}
