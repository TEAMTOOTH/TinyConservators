using UnityEngine;
using UnityEngine.InputSystem;

public class AllowJoiningLevelFlow : MonoBehaviour, ILevelFlowComponent
{
    [SerializeField] PlayerInputManager playerSpawner;
    LevelFlowManager owner;
    public void FinishSection()
    {
        owner.ProgressFlow();
    }

    public void StartSection(LevelFlowManager flowManager)
    {
        owner = flowManager;
        playerSpawner.EnableJoining();
        FinishSection();

    }
}

