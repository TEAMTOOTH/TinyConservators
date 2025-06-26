using UnityEngine;

public class PlayMusicLevelFlow : MonoBehaviour, ILevelFlowComponent
{
    LevelFlowManager owner;
    public void FinishSection()
    {
        owner.ProgressFlow();
    }

    public void StartSection(LevelFlowManager flowManager)
    {
        owner = flowManager;

        //Code for starting music here

        FinishSection();
    }
}
