using System.Collections.Generic;
using UnityEngine;

public class LevelFlowManager : MonoBehaviour
{
    ILevelFlowComponent[] levelFlows;

    int levelFlowIndex;

    private void Start()
    {
        levelFlows = GetComponentsInChildren<ILevelFlowComponent>();

        levelFlows[0].StartSection(this);
    }

    public void ProgressFlow()
    {
        levelFlowIndex++;

        if(levelFlowIndex < levelFlows.Length)
        {
            levelFlows[levelFlowIndex].StartSection(this);
        }
        else
        {
            Debug.Log("Finished all level segments");
        }
        
    }
}
