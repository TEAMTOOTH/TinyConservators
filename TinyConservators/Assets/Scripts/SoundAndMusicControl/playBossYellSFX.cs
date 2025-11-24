using UnityEngine;

public class playBossYellSFX : MonoBehaviour, ILevelFlowComponent
{

    LevelFlowManager owner;

    public void FinishSection()
    {
        owner.ProgressFlow();
    }

    public void StartSection(LevelFlowManager flowManager)
    {
        owner = flowManager;

        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/bossYell");

        FinishSection();

    }
}
