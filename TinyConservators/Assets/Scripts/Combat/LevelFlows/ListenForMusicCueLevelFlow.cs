using System.Collections;
using UnityEngine;

public class ListenForMusicCueLevelFlow : MonoBehaviour, ILevelFlowComponent
{
    //Editor variables
    [SerializeField] string eventName;
    [SerializeField] float pollingRate;
    [SerializeField] float waitTimeBeforeBackupSkip;

    LevelFlowManager owner;
    public void FinishSection()
    {
        owner.ProgressFlow();
    }

    public void StartSection(LevelFlowManager flowManager)
    {
        owner = flowManager;

        StartCoroutine(Listener());
    }

    IEnumerator Listener()
    {
        float timer = 0;
        float pollingTime = 0f;

        float result;
        while (timer < waitTimeBeforeBackupSkip)
        {
            timer += Time.deltaTime;
            pollingTime += Time.deltaTime;
            if(pollingTime > pollingRate)
            {
                //FMODUnity.RuntimeManager.StudioSystem.getParameterByID(desc.id, out result);
                FMODUnity.RuntimeManager.StudioSystem.getParameterByName(eventName, out _, out result);
                if(result == 1)
                {
                    break;
                }
                pollingTime = 0;
            }

            yield return null;
        }
        FinishSection();
    }

}
