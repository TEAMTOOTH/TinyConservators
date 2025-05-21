using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] float minimumAttackWaitTime;
    [SerializeField] float maximumAttackWaitTime;


    [SerializeField] GameObject bossVisual;

    [Header("Attack timing")]
    [SerializeField] float attackMoveTime;


    [Header("For test")]
    [SerializeField] Vector2 startPos;

    GameObject owner;

    BossStates state;
    EnemySpawner enemySpawner;

    List<Enemy> currentMinions;

    int minionAttackAmount;

    bool lastRound;

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
        currentMinions = new List<Enemy>();
        //InvokeRepeating("RepeatingPopUp", 0f, 5f);
        State = BossStates.idle;

        enemySpawner = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemySpawner>();
        //State = BossStates.readying;
    }


    void StateChanged(BossStates from, BossStates to)
    {
        switch (to)
        {
            case BossStates.readying:
                ReadyNextAttack(UnityEngine.Random.Range(minimumAttackWaitTime, maximumAttackWaitTime));
                break;
            case BossStates.attack:
                Attack();
                break;
            case BossStates.eating:
                GetComponent<BossAttack>().Attack();
                break;
            case BossStates.hurt:
                Hurt();
                break;
            case BossStates.walkOff:
                LeaveScreen(3f, false);
                break;
            default:
                break;
        }
    }

    public void InitializeAttackRound(float timeToNextAttack, GameObject owner, int amountOfMinions, float minimumTime, float maximumTime, float attackTime, bool lastRound)
    {
        this.owner = owner;
        minionAttackAmount = amountOfMinions;
        GetComponent<BossAttack>().SetMaxEatingTime(attackTime);
        minimumAttackWaitTime = minimumTime;
        maximumAttackWaitTime = maximumTime;

        this.lastRound = lastRound;

        ReadyNextAttack(timeToNextAttack);
       
    }

    void ReadyNextAttack(float timeToNextAttack)
    {
        StartCoroutine(ReadyAttack());
        IEnumerator ReadyAttack()
        {
            yield return new WaitForSeconds(timeToNextAttack);
            State = BossStates.attack;
            //GetComponentInChildren<Animator>().Play("BossHungry");
            GetComponentInChildren<Animator>().Play("BossBat");
        }
    }
    
    void Attack()
    {
        //bossVisual.transform.localScale = new Vector3(bossAttackSize, bossAttackSize, bossAttackSize);
        GameObject attackSpot = GetComponent<BossMovement>().FindAttackSpot();

        currentMinions.RemoveAll(obj => obj == null);

        int amountToSpawnForRound = minionAttackAmount - currentMinions.Count;
        Enemy[] m = enemySpawner.SpawnEnemies(amountToSpawnForRound);
        
        for(int i = 0; i < m.Length; i++)
        {
            currentMinions.Add(m[i]);
        }

        StartCoroutine(MoveToAttackPoint());
        IEnumerator MoveToAttackPoint()
        {
            GetComponent<BossMovement>().Move(attackMoveTime, GetComponent<BossMovement>().GetFurthestAwayScatterPoint(attackSpot.transform.position).transform.position, attackSpot.transform.position);
            yield return new WaitForSeconds(attackMoveTime);
            //Do get ready to eat visual?
            State = BossStates.eating;
        }
    }

    void Hurt()
    {
        float leaveTime = 0.5f;
        GetComponentInChildren<BossDamage>().AllowCollisions(false);
        GetComponent<BossAttack>().InterruptAttack();
        
        foreach (Enemy e in currentMinions)
        {
            if (e != null)
            {
                e.InstantDissapear();
            }
        }

        if (lastRound)
        {
            GetComponentInChildren<Animator>().Play("BossHurt"); //Should be boss escaping. Or scream face
            LeaveLevel(1.5f);
        }
        else
        {
            GetComponentInChildren<Animator>().Play("BossHurt");
            LeaveScreen(leaveTime, true);
        }
        


        

        

        //currentMinions.


    }

    void LeaveScreen(float time, bool hasBeenChasedAway)
    {
        GameObject closest = GetComponent<BossMovement>().GetClosestScatterPoint(transform.position);

        GetComponent<BossMovement>().Move(time, transform.position, closest.transform.position);

        StartCoroutine(Move());
        IEnumerator Move()
        {
            yield return new WaitForSeconds(time);
            if (hasBeenChasedAway)
            {
                if (owner != null)
                {
                    owner.GetComponent<ILevelFlowComponent>()?.FinishSection();
                    owner = null;
                }
            }
            else
            {
                State = BossStates.readying;
            }
            
        }
    }

    void LeaveLevel(float time)
    {
        GetComponent<BossMovement>().MoveTowardsScreen(time);
        StartCoroutine(Move());
        IEnumerator Move()
        {
            yield return new WaitForSeconds(time);
            if (owner != null)
            {
                owner.GetComponent<ILevelFlowComponent>()?.FinishSection();
            }
            gameObject.SetActive(false);
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
