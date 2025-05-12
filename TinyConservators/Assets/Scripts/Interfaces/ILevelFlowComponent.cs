using UnityEngine;

public interface ILevelFlowComponent
{
    void StartSection(LevelFlowManager flowManager);

    void FinishSection();
}
