using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [SerializeField] GameObject attackSpots;
    [SerializeField] AttackBubbleVisual bubble; 

    List<AttackPoint> availibleAttackPoints;
    List<AttackPoint> mostRecentlyAttackedPoints;

    AttackPoint currentAttackPoint;

    float maxEatTime;
    bool eating;

    //Boss protection
    int amountOfProtection;
    float speedOfProtection;
    float sizeOfProtection;

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
        float time = 0;
        float damageTimer = 0;
        float damageInterval = maxEatTime / currentAttackPoint.GetAmountOfVisualDamageSteps();
        eating = true;

        float damageToPoint = 0;

        StartCoroutine(Eat());
        //Spawn bubble and decal.
        IEnumerator Eat()
        {
            //Debug.Log("Eating");
            //GetComponentInChildren<Animator>().Play("BossPoof");
            GetComponent<Boss>().PlayAnimationIfHasState("BossPoof");

            if(GetComponent<RadialPush2D>() != null)
            {
                GetComponent<RadialPush2D>().PushAway();
            }
            yield return new WaitForSeconds(.25f);
            //GetComponentInChildren<Animator>().Play("BossAttack");
            GetComponent<Boss>().PlayAnimationIfHasState("BossAttack");
            GetComponentInChildren<BossDamage>().AllowCollisions(true);
            
            if(bubble != null)
            {
                bubble.StartShowing();
            }
            
            GetComponent<BossItemManager>().SpawnObjects(amountOfProtection, speedOfProtection, sizeOfProtection);
            currentAttackPoint.NewAttack();



            while (eating)
            {
                time += Time.deltaTime;
                //damageTimer += Time.deltaTime;
                
                if(bubble != null)
                {
                    bubble.ChangeBubbleSize(time, maxEatTime);
                }
                
                currentAttackPoint.Damage(time/maxEatTime);

                if (time > maxEatTime)
                {
                    //Debug.Log("Heading off");
                    eating = false;
                    GetComponent<Boss>().State = BossStates.walkOff;
                    //GetComponentInChildren<Animator>().Play("BossPoof");
                    GetComponent<Boss>().PlayAnimationIfHasState("BossPoof");
                    yield return new WaitForSeconds(.25f);
                    //GetComponentInChildren<Animator>().Play("BossFull");
                    GetComponent<Boss>().PlayAnimationIfHasState("BossFull");

                    if (bubble != null)
                    {
                        bubble.PopBubble();
                    }

                    GetComponentInChildren<BossDamage>().AllowCollisions(false);
                    GetComponent<BossItemManager>().DespawnObjects();
                    GetComponent<Boss>().FadeCurrentMaze();


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

        GetComponent<BossItemManager>().DespawnObjects();
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
