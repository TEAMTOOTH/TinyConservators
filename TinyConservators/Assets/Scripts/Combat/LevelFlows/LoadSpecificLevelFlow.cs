using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSpecificLevelFlow : MonoBehaviour, ILevelFlowComponent
{
    [SerializeField] int levelToLoad;
    LevelFlowManager owner;
    public void FinishSection()
    {
        throw new System.NotImplementedException();
    }

    public void StartSection(LevelFlowManager flowManager)
    {
        owner = flowManager;
        SceneManager.LoadScene(levelToLoad);
    }
}
