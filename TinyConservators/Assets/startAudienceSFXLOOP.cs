using UnityEngine;

public class startAudienceSFXLOOP : MonoBehaviour, ILevelFlowComponent
{
    LevelFlowManager owner;

    public void FinishSection()
    {
        owner.ProgressFlow();
    }

    public void StartSection(LevelFlowManager flowManager)
    {
        owner = flowManager;

        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/titleScreenAudienceLOOP"); // start audience SFX loop
        FinishSection();

    }
}