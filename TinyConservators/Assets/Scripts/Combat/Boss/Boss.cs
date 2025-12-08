using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour, IDamageReceiver
{
    [SerializeField] float minimumAttackWaitTime;
    [SerializeField] float maximumAttackWaitTime;

    [SerializeField] int pointsForKnockingOut;

    [SerializeField] GameObject protectionBubble;

    [SerializeField] GameObject bossVisual;
    [SerializeField] GameObject[] bossMazes;
    
    [SerializeField] GameObject attackSpotPosition;

    [SerializeField] Vector2 attackStartPos;

    [SerializeField] int levelIndex; //Naughty naughty, but at this point, it should be fine :D

    [Header("Attack timing")]
    [SerializeField] float attackMoveTime;

    int mazeIndex = 0;

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
        //transform.position = startPos;
        currentMinions = new List<Enemy>();
        //InvokeRepeating("RepeatingPopUp", 0f, 5f);
        State = BossStates.idle;

        enemySpawner = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemySpawner>();
        //State = BossStates.readying;
        //PlayAnimationIfHasState("Hurt");
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
                break;
            case BossStates.walkOff:
                LeaveScreen(1f, 3f, false);
                break;
            default:
                break;
        }
    }

    public void InitializeAttackRound(float timeToNextAttack, GameObject owner, int amountOfMinions, float minimumTime, float maximumTime, float attackTime, bool lastRound, int amountOfProtection, float speedOfProtection, float sizeOfProtection)
    {
        this.owner = owner;
        minionAttackAmount = amountOfMinions;
        BossAttack bossAttack = GetComponent<BossAttack>();
        bossAttack.SetMaxEatingTime(attackTime);
        bossAttack.SetBossProtectionParameters(amountOfProtection, speedOfProtection, sizeOfProtection);
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
            PlayAnimationIfHasState("Normal");
            yield return new WaitForSeconds(timeToNextAttack);
            State = BossStates.attack;
            //GetComponentInChildren<Animator>().Play("BossHungry");
            
        }
    }
    
    void Attack()
    {
        //gameObject.layer = LayerMask.NameToLayer("Default");
        //bossVisual.transform.localScale = new Vector3(bossAttackSize, bossAttackSize, bossAttackSize);
        GameObject attackSpot = GetComponent<BossMovement>().FindAttackSpot();

        PlayAnimationIfHasState("Normal");


        if (attackSpot == null)
            return;
        
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
            protectionBubble.SetActive(true);
            //GetComponent<BossMovement>().Move(attackMoveTime, GetComponent<BossMovement>().GetFurthestAwayScatterPoint(attackSpot.transform.position).transform.position, attackSpot.transform.position);
            GetComponent<BossMovement>().Move(attackMoveTime, attackStartPos, attackSpotPosition.transform.position);
            yield return new WaitForSeconds(attackMoveTime);
            //Do get ready to eat visual?
            protectionBubble.SetActive(false);
            State = BossStates.eating;
        }
    }

    void Hurt(GameObject hurter)
    {
        float leaveTime = 0.5f;
        float pauseBeforeLeaving = 1f;
        GetComponentInChildren<BossDamage>().AllowCollisions(false);
        GetComponent<BossAttack>().InterruptAttack();
        GetComponent<RadialPush2D>().PushAway(2f, 10f, false);
        //For better flow, "destroy" maze here

        //FadeCurrentMaze();



        //gameObject.layer = LayerMask.NameToLayer("Knockout");

        //This part is hopefully temporary
        SoundController sc = GetComponent<SoundController>();
        if (sc != null)
        {
            GetComponent<SoundController>().PlayClip(0);
        }
        
        if(hurter != null)
        {
            hurter.GetComponent<PointsReceiver>().AddPoints(pointsForKnockingOut);
        }

        foreach (Enemy e in currentMinions)
        {
            if (e != null)
            {
                e.InstantDissapear();
            }
        }




        // set boss got hit to true
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("bossGotHit", 1);

        //Turn off the boss voice layer
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("bossVoice", 0);

        //let's try putting boss hurt SFX here
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/bossHurt");


        StartCoroutine(PassTime());
        IEnumerator PassTime()
        {
            float time = 0;
            while (time < 10)
            {
                time += Time.deltaTime;
                yield return null;
            }
            

            // set boss got hit to back to false
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("bossGotHit", 0);
        }

        if (lastRound)
        {
            //Should be boss escaping. Or scream face
            PlayAnimationIfHasState("Hurt");
            GameObject g = GameObject.FindGameObjectWithTag("StatTracker");
            if(g != null)
            {
                g.GetComponent<StatTracker>().SetBossInterstitialState(levelIndex, true);
            }

            LeaveLevel(3f);
        }
        else
        {
            PlayAnimationIfHasState("Hurt");
            LeaveScreen(pauseBeforeLeaving,leaveTime, true);
        }
        


        

        

        //currentMinions.


    }

    public void FadeCurrentMaze()
    {
        if (bossMazes.Length <= 0)
            return;

        if (bossMazes[mazeIndex] == null)
            return;


        bossMazes[mazeIndex].GetComponentInChildren<SpriteFader>().FadeTo(0f, 3f);

        TouchKnockout[] colliders = bossMazes[mazeIndex].GetComponentsInChildren<TouchKnockout>();
        //Debug.Log("TouchKnockout is " + colliders.Length);

        for(int i = 0; i < colliders.Length; i++)
        {
            colliders[i].gameObject.SetActive(false);
        }

        //mazeToFade.SetActive(false);
        mazeIndex++;
    }

    void LeaveScreen(float initialWaitTime, float time, bool hasBeenChasedAway)
    {
        GameObject closest = GetComponent<BossMovement>().GetClosestScatterPoint(transform.position);

        

        StartCoroutine(Move());
        IEnumerator Move()
        {
            
            GetComponent<BossMovement>().Move(time, transform.position, closest.transform.position);

            if (hasBeenChasedAway)
            {
                SpawnAccruedDamage(GetComponent<BossAttack>().GetMostRecentlyAttackedPoints());
                
                PlayAnimationIfHasState("Hurt");
            }
            else
            {
                PlayAnimationIfHasState("Normal");
            }
            
            yield return new WaitForSeconds(time);
            
            
            if (owner != null)
            {
                owner.GetComponent<ILevelFlowComponent>()?.FinishSection();
                owner = null;
            }
             
        }
    }

    void LeaveLevel(float time)
    {
        GetComponent<BossMovement>().MoveTowardsScreen(time);
        StartCoroutine(Move());
        IEnumerator Move()
        {
            SpawnAccruedDamage(GetComponent<BossAttack>().GetMostRecentlyAttackedPoints());
            yield return new WaitForSeconds(time);
            GetComponent<BossAttack>().ClearMostRecentlyAttackedPoints();
            if (owner != null)
            {
                owner.GetComponent<ILevelFlowComponent>()?.FinishSection();
            }
            
            gameObject.SetActive(false);
        }
    }

    void SpawnAccruedDamage(List<AttackPoint> damagePoints) //Pretty messy decision tree, but keeping it small, so should be fine. If it gets any bigger, time to rethink!
    {
        Debug.Log(damagePoints.Count);
        if(damagePoints.Count == 0)//Should never really trigger, but better to be safe than sorry.
        {
            GetComponent<PickupSpawner>().SpawnPickups();
        }
        else if(damagePoints.Count == 1)
        {
            GetComponent<PickupSpawner>().SpawnPickups(damagePoints[0].gameObject);
        }
        else
        {
            List<GameObject> gos = new List<GameObject>();

            foreach (AttackPoint a in damagePoints)
            {
                gos.Add(a.gameObject);
            }
            GetComponent<PickupSpawner>().SpawnPickups(gos);
        }
    }

    void IDamageReceiver.Hurt(GameObject gameObject)
    {
        State = BossStates.hurt;
        Hurt(gameObject);
    }

    public void PlayAnimationIfHasState(string state)
    {
        var anim = GetComponentsInChildren<Animator>();

        if(anim.Length > 0)
        {
            var stateId = Animator.StringToHash(state);
            for (int i = 0; i < anim.Length; i++)
            {
                var hasState = GetComponentInChildren<Animator>().HasState(0, stateId);

                if (hasState)
                {
                    
                    anim[i].Play(state);
                }
                else
                {
                    Debug.Log("Does not have the state: " + state);
                }
            }
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
