using UnityEngine;

public class BossAttackLevelFlow : MonoBehaviour, ILevelFlowComponent
{
    [SerializeField] Boss boss;

    [Header("Event parameters")]
    [SerializeField] int amountOfAccompaningMinions;
    [SerializeField] float timeBeforeInitialAttack;
    
    [SerializeField] float minimumRepeatTime;
    [SerializeField] float maximumRepeatTime;
    
    
    [SerializeField] float maxAttackTime;
    
    [SerializeField] bool lastAttack;


    [Header("Boss parameters")]
    [SerializeField] int amountOfProtectionItems;
    [SerializeField] float totalTimeOfRevolutionOfProtectionItems;
    [SerializeField] float radiusOfProtectionCircle;

    LevelFlowManager owner;
    public void FinishSection()
    {
        owner.ProgressFlow();
    }

    public void StartSection(LevelFlowManager flowManager)
    {
        owner = flowManager;
        boss.InitializeAttackRound(timeBeforeInitialAttack, gameObject, amountOfAccompaningMinions, minimumRepeatTime, maximumRepeatTime, maxAttackTime, lastAttack, amountOfProtectionItems, totalTimeOfRevolutionOfProtectionItems, radiusOfProtectionCircle);
    }
}

