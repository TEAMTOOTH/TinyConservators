using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalizeGame : MonoBehaviour, ILevelFlowComponent
{
    LevelFlowManager owner;
    public void FinishSection()
    {
        owner.ProgressFlow();
    }

    public void StartSection(LevelFlowManager flowManager)
    {
        owner = flowManager;
        GameObject.FindGameObjectWithTag("DontDestroyManager")?.GetComponent<DontDestroyOnLoadManager>().DestroyAllDontDestroyObjects();
        FinishSection();
    }
}
