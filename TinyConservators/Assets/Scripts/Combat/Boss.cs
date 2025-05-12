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
        State = BossStates.readying;
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
                GetComponentInChildren<BossDamage>().AllowCollisions(true);
                break;
            case BossStates.hurt:
                LeaveScreen(0.5f);
                GetComponentInChildren<BossDamage>().AllowCollisions(false);
                GetComponent<BossAttack>().StopAllCoroutines();
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
    void ReadyNextAttack(float timeToNextAttack)
    {
        StartCoroutine(ReadyAttack());

        IEnumerator ReadyAttack()
        {
            float timeToPopUp = timeToNextAttack - popUpTime; //Use variables here

            yield return new WaitForSeconds(timeToPopUp);

            PopUp(1, popUpTime - 2);

            yield return new WaitForSeconds(timeToNextAttack-timeToPopUp);

            State = BossStates.attack;

        }
    }

    void PopUp(float movementTime, float waitTime)
    {
        bossVisual.transform.localScale = new Vector3(bossPopupSize, bossPopupSize, bossPopupSize);
        //transform.position = popUpPositions[UnityEngine.Random.Range(0, popUpPositions.Length)]; Skip for test
        
        StartCoroutine(Move());

        IEnumerator Move()
        { 
            GetComponent<BossMovement>().Move(movementTime, startPos, endPos);
            yield return new WaitForSeconds(movementTime + waitTime);
            GetComponent<BossMovement>().Move(movementTime, endPos, startPos);
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

    // Update is called once per frame
    void Update()
    {
        
    }
}

public enum BossStates
{
    idle,
    readying,
    attack,
    eating,
    hurt,
    walkOff
}
