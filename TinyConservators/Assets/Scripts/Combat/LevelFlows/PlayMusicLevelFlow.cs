using UnityEngine;

public class PlayMusicLevelFlow : MonoBehaviour, ILevelFlowComponent
{
    LevelFlowManager owner;
    private FMOD.Studio.EventInstance musicSelector;
    public void FinishSection()
    {
        owner.ProgressFlow();
    }

    public void StartSection(LevelFlowManager flowManager)
    {
        owner = flowManager;

        //Code for starting music here
        musicSelector = FMODUnity.RuntimeManager.CreateInstance("event:/music/musicWithLyrics");
        musicSelector.start();
        FinishSection();
    }
}
