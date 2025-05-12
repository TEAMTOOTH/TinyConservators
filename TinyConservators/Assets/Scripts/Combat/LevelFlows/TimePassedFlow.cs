using System.Collections;
using UnityEngine;

public class TimePassedFlow : MonoBehaviour, ILevelFlowComponent
{
    [SerializeField] float length;
    
    LevelFlowManager owner;
    public void FinishSection()
    {
        owner.ProgressFlow();

    }

    public void StartSection(LevelFlowManager flowManager)
    {
        owner = flowManager;
        StartCoroutine(PassTime());
        IEnumerator PassTime()
        {
            //Doing it the roundabout way to get greater fidelity and allow for any tiny amount of time.
            float time = 0;
            while(time < length)
            {
                time += Time.deltaTime;
                yield return null;
            }
            FinishSection();
        }
    }
}
