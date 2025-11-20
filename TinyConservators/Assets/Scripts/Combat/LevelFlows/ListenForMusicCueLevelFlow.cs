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
        while (timer < 30)
        {
            timer += Time.deltaTime;
            pollingTime += Time.deltaTime;
            if(pollingTime > .25f)
            {
                FMODUnity.RuntimeManager.StudioSystem.getParameterByName("ladyIsSinging", out float result);
                Debug.Log(timer + " " + result);
                pollingTime = 0;
            }

            yield return null;
        }
        FinishSection();
        Debug.Log("Trigger lady");
    }

}
