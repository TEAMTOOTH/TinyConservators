using UnityEngine;

public class TeleportObjectFlow : MonoBehaviour, ILevelFlowComponent
{
    [SerializeField] GameObject objectToTeleport;
    [SerializeField] Vector3 positionToTeleportTo;


    LevelFlowManager owner;
    public void FinishSection()
    {
        owner.ProgressFlow();
    }

    public void StartSection(LevelFlowManager flowManager)
    {
        owner = flowManager;
        objectToTeleport.transform.position = positionToTeleportTo;
        FinishSection();
    }   
}
