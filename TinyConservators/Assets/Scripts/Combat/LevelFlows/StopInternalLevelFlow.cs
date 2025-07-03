using UnityEngine;

public class StopInternalLevelFlow : MonoBehaviour, ILevelFlowComponent
{
    LevelFlowManager owner;
    public void FinishSection()
    {
        
    }

    public void StartSection(LevelFlowManager flowManager)
    {
        owner = flowManager;
        Debug.Log("Stopping flow, awaiting external restart");
    }

}
