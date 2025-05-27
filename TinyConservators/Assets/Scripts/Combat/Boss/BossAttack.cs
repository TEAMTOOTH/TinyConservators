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
        AttackPoint chosenAttackPoint = null;
        if(availibleAttackPoints.Count > 0)
        {
            chosenAttackPoint = availibleAttackPoints[Random.Range(0, availibleAttackPoints.Count)];
            availibleAttackPoints.Remove(chosenAttackPoint);
        }
        currentAttackPoint = chosenAttackPoint;
        mostRecentlyAttackedPoints.Add(chosenAttackPoint);
        return chosenAttackPoint;
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
            GetComponentInChildren<Animator>().Play("BossPoof");
            yield return new WaitForSeconds(.25f);
            GetComponentInChildren<Animator>().Play("BossAttack");
            GetComponentInChildren<BossDamage>().AllowCollisions(true);
            bubble.StartShowing();
            GetComponent<BossItemManager>().SpawnObjects(amountOfProtection, speedOfProtection, sizeOfProtection);



            while (eating)
            {
                time += Time.deltaTime;
                //damageTimer += Time.deltaTime;
                bubble.ChangeBubbleSize(time, maxEatTime);
                
                currentAttackPoint.Damage(time/maxEatTime);

                if (time > maxEatTime)
                {
                    //Debug.Log("Heading off");
                    eating = false;
                    GetComponent<Boss>().State = BossStates.walkOff;
                    GetComponentInChildren<Animator>().Play("BossFull");
                    bubble.PopBubble();
                    GetComponentInChildren<BossDamage>().AllowCollisions(false);
                    GetComponent<BossItemManager>().DespawnObjects();

                }
                yield return null;

            }        
        } 
    }

    public void InterruptAttack()
    {
        bubble.PopBubble();
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
        return mostRecentlyAttackedPoints;
    }
}
