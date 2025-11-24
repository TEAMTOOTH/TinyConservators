using UnityEngine;

public class LevelFlowPlaySFXOneShot : MonoBehaviour, ILevelFlowComponent
{
    [SerializeField] string nameOfSFX;
    LevelFlowManager owner;

    public void FinishSection()
    {
        owner.ProgressFlow();
    }

    public void StartSection(LevelFlowManager flowManager)
    {
        owner = flowManager;

        // Open door SFX
        string SFXName = "event:/SFX/" + nameOfSFX;
        Debug.Log(SFXName);
        FMODUnity.RuntimeManager.PlayOneShot(SFXName);

        FinishSection();

    }
}
