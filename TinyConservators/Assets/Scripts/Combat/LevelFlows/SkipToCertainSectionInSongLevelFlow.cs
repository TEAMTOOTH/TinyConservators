using UnityEngine;

public class SkipToCertainSectionInSongLevelFlow : MonoBehaviour, ILevelFlowComponent
{
    [SerializeField] int skipIndex;
    LevelFlowManager owner;
    public void FinishSection()
    {
        owner.ProgressFlow();
    }

    public void StartSection(LevelFlowManager flowManager)
    {
        owner = flowManager;
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("interstitialSkipNumber", skipIndex);
        FinishSection();
    }
}
