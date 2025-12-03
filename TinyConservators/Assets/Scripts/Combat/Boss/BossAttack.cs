using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [SerializeField] GameObject attackSpots;
    [SerializeField] AttackBubbleVisual bubble;
    [SerializeField] int damageStepsPerAttack;
    
    [SerializeField] PickupSpawner damageVisualSpawner;

    [SerializeField] BossAttackOverlord bossItemManagers;

    List<AttackPoint> availibleAttackPoints;
    List<AttackPoint> mostRecentlyAttackedPoints;

    AttackPoint currentAttackPoint;

    float maxEatTime;
    bool eating;

    //Boss protection
    int amountOfProtection;
    float speedOfProtection;
    float sizeOfProtection;

    int amountOfTimesAttacked = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        availibleAttackPoints = new List<AttackPoint>();
        mostRecentlyAttackedPoints = new List<AttackPoint>();   
        var aP = attackSpots.GetComponentsInChildren<AttackPoint>();
        for(int i = 0; i < aP.Length; i++)
        {
            availibleAttackPoints.Add(aP[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public AttackPoint[] GetAttackSpots()
    {
        return availibleAttackPoints.ToArray();
    }

    public AttackPoint ChooseNextAttackPoint()
    {

        /*AttackPoint chosenAttackPoint = null;
        if(availibleAttackPoints.Count > 0)
        {
            chosenAttackPoint = availibleAttackPoints[Random.Range(0, availibleAttackPoints.Count)];
            availibleAttackPoints.Remove(chosenAttackPoint);
        }*/
        currentAttackPoint = availibleAttackPoints[0];
        //mostRecentlyAttackedPoints.Add(chosenAttackPoint);
        return availibleAttackPoints[0];
    }

    public void Attack()
    {
        StartCoroutine(Eat());

        IEnumerator Eat()
        {
            // Setup phase
            GetComponent<Boss>().PlayAnimationIfHasState("Normal");
            //GetComponent<RadialPush2D>()?.PushAway();

            yield return new WaitForSeconds(0.25f);

            GetComponent<Boss>().PlayAnimationIfHasState("Normal");
            GetComponentInChildren<BossDamage>().AllowCollisions(true);

            if (bubble != null)
                bubble.StartShowing();

            bossItemManagers?.Attack(amountOfTimesAttacked);
            
            

            //currentAttackPoint.NewAttack();

            currentAttackPoint.Damage(damageStepsPerAttack);
            damageVisualSpawner.SpawnFakePickups();

            float elapsed = 0f;

            eating = true;

            // Eating phase loop
            while (eating)
            {
                elapsed += Time.deltaTime;

                if (bubble != null)
                    bubble.ChangeBubbleSize(elapsed, maxEatTime);

                // End phase
                if (elapsed >= maxEatTime)
                {
                    eating = false;

                    bossItemManagers?.Despawn(amountOfTimesAttacked);
                    GetComponent<Boss>().State = BossStates.walkOff;
                    GetComponent<Boss>().PlayAnimationIfHasState("Normal");
                    yield return new WaitForSeconds(0.25f);
                    GetComponent<Boss>().PlayAnimationIfHasState("Normal");

                    if (bubble != null)
                        bubble.PopBubble();

                    GetComponentInChildren<BossDamage>().AllowCollisions(false);
                    GetComponent<Boss>().FadeCurrentMaze();
                    amountOfTimesAttacked++;
                }

                yield return null;
            }
        }
    }

    public void InterruptAttack()
    {
        
        if (bubble != null)
        {
            bubble.PopBubble();
        }

        GetComponent<Boss>().FadeCurrentMaze();

        bossItemManagers?.Despawn(amountOfTimesAttacked);
        amountOfTimesAttacked++;

        StopAllCoroutines();
    }

    public void SetMaxEatingTime(float maxEatingTime)
    {
        maxEatTime = maxEatingTime;
    }

    public void SetBossProtectionParameters(int amount, float speed, float size)
    {
        amountOfProtection = amount;
        speedOfProtection = speed;
        sizeOfProtection = size;
    }

    public void ClearMostRecentlyAttackedPoints()
    {
        mostRecentlyAttackedPoints = new List<AttackPoint>();
    }

    public List<AttackPoint> GetMostRecentlyAttackedPoints()
    {
        /*if(mostRecentlyAttackedPoints.Count > 2)
        {
            List<AttackPoint> twoLatestAttackPoints = new List<AttackPoint> { mostRecentlyAttackedPoints[mostRecentlyAttackedPoints.Count - 2], mostRecentlyAttackedPoints[mostRecentlyAttackedPoints.Count - 1] };
            return twoLatestAttackPoints;

        }*/
        //Reusing some of this code to make less work later down the pipeline, used to be a lot more complicated and now there is only one attackpoint
        List<AttackPoint> twoLatestAttackPoints = new List<AttackPoint> { availibleAttackPoints[0] };

        return twoLatestAttackPoints;
    }
}
