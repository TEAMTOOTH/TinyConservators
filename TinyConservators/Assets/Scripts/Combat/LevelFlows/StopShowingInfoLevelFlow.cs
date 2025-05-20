using UnityEngine;

public class StopShowingInfoLevelFlow : MonoBehaviour, ILevelFlowComponent
{
    [SerializeField] GameObject objectToHide;
    LevelFlowManager owner;

    public void FinishSection()
    {
        owner.ProgressFlow();
    }

    public void StartSection(LevelFlowManager flowManager)
    {
        owner = flowManager;

        objectToHide.SetActive(false);
        FinishSection();
    }


}