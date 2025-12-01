using System.Collections;
using UnityEngine;

public class changeToCutsceneMusic : MonoBehaviour, ILevelFlowComponent
{
    LevelFlowManager owner;

    public void FinishSection()
    {
        owner.ProgressFlow();
    }

    public void StartSection(LevelFlowManager flowManager)
    {
        owner = flowManager;

        //Set music skip trigger to go to the next cutscene
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("cutsceneGo", 1);
        FinishSection();

        StartCoroutine(PassTime());
        IEnumerator PassTime()
        {
            float time = 0;
            while (time < 5)
            {
                time += Time.deltaTime;
                yield return null;
            }

            // Reset the music skip trigger
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("cutsceneGo", 0);
            
        }
    }
}
