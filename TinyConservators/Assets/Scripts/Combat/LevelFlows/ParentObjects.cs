using UnityEngine;

public class ParentObjects : MonoBehaviour, ILevelFlowComponent
{
    [SerializeField] GameObject parent;
    [SerializeField] GameObject[] children;
    [SerializeField] bool deparent;
    LevelFlowManager owner;
    public void FinishSection()
    {
        owner.ProgressFlow();
    }

    public void StartSection(LevelFlowManager flowManager)
    {
        owner = flowManager;
        if (deparent)
        {
            for (int i = 0; i < children.Length; i++)
            {
                children[i].transform.parent = null;
            }
        }
        else 
        {
            for (int i = 0; i < children.Length; i++)
            {
                children[i].transform.parent = parent.transform;
            }
        }

        FinishSection();
    }

    
}
