using System;
using System.Collections;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] float minimumAttackWaitTime;
    [SerializeField] float maximumAttackWaitTime;

    [SerializeField] Vector2[] popUpPositions;

    [SerializeField] GameObject bossVisual;
    [SerializeField] float bossPopupSize;
    [SerializeField] float bossAttackSize;
    
    [SerializeField] AnimationCurve moveCurve;

    [Header("Attack timing")]
    [SerializeField] float popUpTime;
    [SerializeField] float attackMoveTime;


    [Header("For test")]
    [SerializeField] Vector2 startPos;
    [SerializeField] Vector2 endPos;

    GameObject owner;

    BossStates state;
    public BossStates State
    {
        get => state;
        set
        {
            if (state != value)
            {
                BossStates oldState = state;
                state = value;
                OnBossStateChanged?.Invoke(oldState, state);
            }
        }
    }

    public event Action<BossStates, BossStates> OnBossStateChanged;

    void Start()
    {
        OnBossStateChanged += (from, to) => StateChanged(from, to);
        transform.position = startPos;
        //InvokeRepeating("RepeatingPopUp", 0f, 5f);
        State = BossStates.idle;

        //State = BossStates.readying;
    }


    void StateChanged(BossStates from, BossStates to)
    {
        switch (to)
        {
            case BossStates.readying:
                ReadyNextAttack(UnityEngine.Random.Range(minimumAttackWaitTime, maximumAttackWaitTime), null);
                break;
            case BossStates.attack:
                Attack();
                break;
            case BossStates.eating:
                GetComponent<BossAttack>().Attack();
                GetComponentInChildren<BossDamage>().AllowCollisions(true);
                break;
            case BossStates.hurt:
                Hurt();
                break;
            case BossStates.walkOff:
                LeaveScreen(3f);
                GetComponentInChildren<BossDamage>().AllowCollisions(false);
                break;
            default:
                break;
        }
    }

    //Probably needs to be refactored at some point, hacking it a bit together for the demo on the 14.05
    public void ReadyNextAttack(float timeToNextAttack, GameObject owner)
    {
        this.owner = owner;

        StartCoroutine(ReadyAttack());

        IEnumerator ReadyAttack()
        {
            yield return new WaitForSeconds(timeToNextAttack);
            State = BossStates.attack;

        }
    }
    
    void Attack()
    {
        bossVisual.transform.localScale = new Vector3(bossAttackSize, bossAttackSize, bossAttackSize);
        GameObject attackSpot = GetComponent<BossMovement>().FindAttackSpot();
        

        StartCoroutine(MoveToAttackPoint());
        IEnumerator MoveToAttackPoint()
        {
            GetComponent<BossMovement>().Move(attackMoveTime, Vector3.zero, attackSpot.transform.position);
            yield return new WaitForSeconds(attackMoveTime);
            //Do get ready to eat visual?
            State = BossStates.eating;
        }
    }

    void Hurt()
    {
        float leaveTime = 0.5f;
        LeaveScreen(leaveTime);
        GetComponent<BossAttack>().StopAllCoroutines();
        GetComponentInChildren<BossDamage>().AllowCollisions(false);
        
        StartCoroutine(WaitBeforeCall());
        IEnumerator WaitBeforeCall()
        {
            yield return new WaitForSeconds(leaveTime);
            if(owner != null)
            {
                owner.GetComponent<ILevelFlowComponent>()?.FinishSection();
            }
        }
        
        
    }

    void LeaveScreen(float time)
    {
        GameObject[] scatterPoints = GameObject.FindGameObjectsWithTag("ScatterPoint");

        GameObject closest = null;
        float shortestDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject obj in scatterPoints)
        {
            Debug.Log("Found a scatterPoint");
            if (obj == null) continue;

            float distance = Vector3.Distance(currentPosition, obj.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closest = obj;
            }
        }

        GetComponent<BossMovement>().Move(time, transform.position, closest.transform.position);

        StartCoroutine(Move());
        IEnumerator Move()
        {
            yield return new WaitForSeconds(time);
            State = BossStates.readying;
        }

    }
}

public enum BossStates
{
    idle,
    popup,
    readying,
    attack,
    eating,
    hurt,
    walkOff
}
