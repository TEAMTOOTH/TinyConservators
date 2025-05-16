using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [SerializeField] GameObject attackSpots;
    [SerializeField] AttackBubbleVisual bubble; 

    List<AttackPoint> availibleAttackPoints;
    AttackPoint currentAttackPoint;

    float maxEatTime;
    bool eating;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        availibleAttackPoints = new List<AttackPoint>();   
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
        return chosenAttackPoint;


        
    }

    public void Attack()
    {
        float time = 0;
        float damageTimer = 0;
        float damageInterval = maxEatTime / currentAttackPoint.GetAmountOfVisualDamageSteps();
        eating = true;
        bubble.StartShowing();

        StartCoroutine(Eat());
        //Spawn bubble and decal.
        IEnumerator Eat()
        {
            //Debug.Log("Eating");
            
            while (eating)
            {
                time += Time.deltaTime;
                damageTimer += Time.deltaTime;
                bubble.ChangeBubbleSize(time, maxEatTime);

                //Debug.Log(time);
                if (damageTimer >= damageInterval)
                {
                    currentAttackPoint.ProgressVisual();
                    Debug.Log("Damage painting");
                    damageTimer = 0f;
                }

                if (time > maxEatTime)
                {
                    //Debug.Log("Heading off");
                    eating = false;
                    GetComponent<Boss>().State = BossStates.walkOff;
                    bubble.PopBubble();
                }
                yield return null;

                //Do damage


            }        
        } 
    }

    public void SetMaxEatingTime(float maxEatingTime)
    {
        maxEatTime = maxEatingTime;
    }
}
