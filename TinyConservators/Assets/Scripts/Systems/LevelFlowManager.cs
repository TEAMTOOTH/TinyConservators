using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFlowManager : MonoBehaviour
{
    ILevelFlowComponent[] levelFlows;

    int levelFlowIndex;

    private void Start()
    {
        levelFlows = GetComponentsInChildren<ILevelFlowComponent>();

        levelFlows[0].StartSection(this);
        Debug.Log("Starting level flow " + levelFlows[0]);
    }

    public void ProgressFlow()
    {
        levelFlowIndex++;
        //Debug.Log("Progressing level flow " + levelFlows[levelFlowIndex]);

        if(levelFlowIndex < levelFlows.Length)
        {
            levelFlows[levelFlowIndex].StartSection(this);
        }
        else
        {
            Debug.Log("Finished all level segments");
        }
        
    }

    public void JumpToFlow(int flowIndex)
    {
        //levelFlowIndex = flowIndex - 1;
        //ProgressFlow();
        GameObject g = GameObject.FindGameObjectWithTag("StatTracker");
        if (g != null)
        {
            g.GetComponent<StatTracker>().SetStats(0);
        }
        SceneManager.LoadScene(3);
    }
    
    public int GetAmountOfFlows()
    {
        return levelFlows.Length;
    }
}
