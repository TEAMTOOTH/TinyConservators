using UnityEngine;

public class MoveVisual : MonoBehaviour, ILevelFlowComponent
{
    [SerializeField] GameObject visualMove;
    [SerializeField] float moveTime;
    [SerializeField] int moveIndex;
    [SerializeField] bool parallell;
    

    IVisualMove [] move;

    LevelFlowManager owner;

    public void FinishSection()
    {
        owner?.ProgressFlow();
    }

    public void StartSection(LevelFlowManager flowManager)
    {
        move = visualMove.GetComponents<IVisualMove>();
        owner = flowManager;

        move[moveIndex].Move(moveTime);

        if (parallell)
        {
            FinishSection();
        }
        else
        {
            Invoke("FinishSection", moveTime);
        }
    }
}
