using UnityEngine;

public class ChangeStateOfLevelObject : MonoBehaviour, ILevelFlowComponent
{
    [SerializeField] GameObject[] objectsToChange;
    [SerializeField] bool active;
    LevelFlowManager owner;


    public void FinishSection()
    {
        owner.ProgressFlow();
    }

    public void StartSection(LevelFlowManager flowManager)
    {
        owner = flowManager;

        for(int i = 0; i < objectsToChange.Length; i++)
        {
            objectsToChange[i].SetActive(active);
        } 
        FinishSection();   
    }


}


