using UnityEngine;

public class BossAttackLevelFlow : MonoBehaviour, ILevelFlowComponent
{
    [SerializeField] Boss boss;
    
    LevelFlowManager owner;
    public void FinishSection()
    {
        owner.ProgressFlow();
    }

    public void StartSection(LevelFlowManager flowManager)
    {
        owner = flowManager;
        boss.ReadyNextAttack(0f, gameObject);
    }

}
