using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnLevelFlow : MonoBehaviour, ILevelFlowComponent, IHappenedCounter
{
    [SerializeField] int amountOfEnemiesToSpawn;
    [SerializeField] float timeLimit = 60;

    List<Enemy> currentMinions;

    int enemiesKilled;
    EnemySpawner spawner;
    LevelFlowManager owner;
    bool isTheActiveFlow;

    IEnumerator countdown;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawner = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemySpawner>();
        
    }

    public void FinishSection()
    {
        isTheActiveFlow = false;
        owner.ProgressFlow();
    }

    public void StartSection(LevelFlowManager flowManager)
    {
        owner = flowManager;
        //spawner.SpawnEnemies(amountOfEnemiesToSpawn, gameObject);
        Enemy[] m = spawner.SpawnEnemies(amountOfEnemiesToSpawn, gameObject);
        isTheActiveFlow = true;
        countdown = SectionTimeLimit();

        currentMinions = new List<Enemy>();
        for (int i = 0; i < m.Length; i++)
        {
            currentMinions.Add(m[i]);
        }

        StartCoroutine(countdown);


    }

    public void ListenedActionHappened()
    {
        enemiesKilled++;
        if(enemiesKilled >= amountOfEnemiesToSpawn && isTheActiveFlow)
        {
            StopCoroutine(countdown);
            FinishSection();
        }
    }

    IEnumerator SectionTimeLimit()
    {
        Debug.Log("Starting timer");

        //using waitforseconds because i dont need frame percision.
        yield return new WaitForSeconds(timeLimit);
        Debug.Log("End timer");

        if (isTheActiveFlow)
        {
            DestroyAnyRemainingMinions();
        }
        
    }

    void DestroyAnyRemainingMinions()
    {
        for(int i = 0; i < currentMinions.Count; i++)
        {
            if(currentMinions[i] != null)
            {
                Enemy e = currentMinions[i].GetComponent<Enemy>();
                if (e != null)
                {
                    //Destroy(e.gameObject);
                    e.InstantDissapear();
                    ListenedActionHappened();
                }
            }
        }

        foreach(Enemy e in currentMinions)
        {
            
        }
        //int amountToSpawnForRound = minionAttackAmount - currentMinions.Count;
        //Enemy[] m = 


    }
}
