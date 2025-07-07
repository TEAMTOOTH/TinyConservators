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
        musicSelector = FMODUnity.RuntimeManager.CreateInstance("event:/music/mainMusicFlow");
        musicSelector.start();

        // Reset cutscene music trigger - this makes the music skip to the cutscene audio
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("cutsceneGo", 0);

        FinishSection();
    }
}
