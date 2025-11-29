using UnityEngine;

public class StartSubtitlesLevelFlow : MonoBehaviour, ILevelFlowComponent
{
    [SerializeField] SubtitleDisplayer subtitleController;
    LevelFlowManager owner;

    public void FinishSection()
    {
        owner.ProgressFlow();
    }

    public void StartSection(LevelFlowManager flowManager)
    {
        owner = flowManager;
        SubtitleDisplayer subtitles;
        if (subtitleController == null)
        {
            subtitles = GameObject.FindGameObjectWithTag("Subtitles").GetComponent<SubtitleDisplayer>();
        }
        else
        {
            subtitles = subtitleController;
        }
        

        if (subtitles != null)
        {
            subtitles.StartSubtitles();
        }
        else
        {
            Debug.LogWarning("Can't find subtitle controller, have you added it to the scene or remembered to tag it correctly?");
        }
        FinishSection();
    }

}
