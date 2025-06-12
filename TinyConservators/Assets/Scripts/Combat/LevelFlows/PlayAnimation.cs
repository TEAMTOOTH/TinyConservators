using UnityEngine;

public class PlayAnimation : MonoBehaviour, ILevelFlowComponent
{
    [SerializeField] GameObject animationObject;
    [SerializeField] float timeBeforeNextNode;
    [SerializeField] string nameOfAnimation;

    LevelFlowManager owner;
    public void FinishSection()
    {
        owner.ProgressFlow();
    }

    public void StartSection(LevelFlowManager flowManager)
    {
        owner = flowManager;
        animationObject.GetComponent<Animator>().Play(nameOfAnimation);
        Invoke("FinishSection", timeBeforeNextNode);
    }
}
