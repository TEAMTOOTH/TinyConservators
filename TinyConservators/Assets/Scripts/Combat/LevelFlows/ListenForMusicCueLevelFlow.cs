using System.Collections;
using UnityEngine;

public class ListenForMusicCueLevelFlow : MonoBehaviour, ILevelFlowComponent
{
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
        Debug.Log("In ListenForMusicLevelFlow");


        
        float timer = 0;
        float pollingTime = 0f;

        float result;
        while (timer < 10)
        {
            timer += Time.deltaTime;
            pollingTime += Time.deltaTime;
            if(pollingTime > .25f)
            {
                FMODUnity.RuntimeManager.StudioSystem.getParameterByName("ladyIsSinging", out result);
                if(result == 1)
                {
                    Debug.Log("Triggered");
                }
                //Debug.Log(result);
                pollingTime = 0;
            }

            yield return null;
        }
        FMODUnity.RuntimeManager.StudioSystem.getParameterByName("ladyIsSinging", out result);
        Debug.Log(result);
        FinishSection();
        Debug.Log("Trigger lady");
    }

}
