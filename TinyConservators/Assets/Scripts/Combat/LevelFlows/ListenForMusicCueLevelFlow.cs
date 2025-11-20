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


        FMODUnity.RuntimeManager.StudioSystem.getParameterByName("ladyIsSinging", out float result);
        float timer = 0;
        while (timer < 30)
        {
            
            timer += Time.deltaTime;

            Debug.Log(timer + " " + result);

            yield return null;
        }
        FinishSection();
        Debug.Log("Trigger lady");
    }

}
