using UnityEngine;

public class EnemySpawnFlow : MonoBehaviour, ILevelFlowComponent, IHappenedCounter
{
    [SerializeField] int amountOfEnemiesToSpawn;

    int enemiesKilled;
    EnemySpawner spawner;
    LevelFlowManager owner;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawner = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemySpawner>();
    }

    public void FinishSection()
    {
        owner.ProgressFlow();
    }

    public void StartSection(LevelFlowManager flowManager)
    {
        owner = flowManager;
        spawner.SpawnEnemies(amountOfEnemiesToSpawn, gameObject);
    }

    public void ListenedActionHappened()
    {
        enemiesKilled++;
        Debug.Log(enemiesKilled);
        if(enemiesKilled >= amountOfEnemiesToSpawn)
        {
            FinishSection();
        }
    }
}
