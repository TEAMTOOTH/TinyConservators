using UnityEngine;

public class stopAudienceSFXLOOP : MonoBehaviour, ILevelFlowComponent
{
    LevelFlowManager owner;

    public void FinishSection()
    {
        owner.ProgressFlow();
    }

    public void StartSection(LevelFlowManager flowManager)
    {
        owner = flowManager;

        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/stopAudienceSFXLOOP"); // stops audience SFX loop

        FinishSection();

    }
}