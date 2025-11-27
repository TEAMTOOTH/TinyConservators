using System.Collections.Generic;
using System;
using UnityEngine;

public class SpawnDamageObjectsLevelFlow : MonoBehaviour, ILevelFlowComponent
{
    [SerializeField] BossItemManager[] objectSpawner;
    [SerializeField] bool despawn;

    [SerializeField]DamageObjectInformation[] attackInformation;

    LevelFlowManager owner;
    public void FinishSection()
    {
        owner.ProgressFlow();
    }

    public void StartSection(LevelFlowManager flowManager)
    {
        owner = flowManager;

        if (despawn)
        {
            foreach (BossItemManager bim in objectSpawner)
            {
                bim.DespawnObjects();
            }
        }
        else
        {
            for(int i = 0; i < objectSpawner.Length; i++)
            {
                //objectSpawner[i].SpawnObjects(attackInformation[i].amount, attackInformation[i].revolutionTime, attackInformation[i].size);
            }
        }

        FinishSection();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
