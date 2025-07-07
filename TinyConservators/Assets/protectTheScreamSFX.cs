using UnityEngine;

public class protectTheScreamSFX : MonoBehaviour, ILevelFlowComponent
{

    LevelFlowManager owner;

    public void FinishSection()
    {
        owner.ProgressFlow();
    }

    public void StartSection(LevelFlowManager flowManager)
    {
        owner = flowManager;

        // Open door SFX
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/protectTheScream");

        FinishSection();

    }
}