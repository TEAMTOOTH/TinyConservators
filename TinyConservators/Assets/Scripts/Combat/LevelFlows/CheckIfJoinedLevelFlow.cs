using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CheckIfJoinedLevelFlow : MonoBehaviour, ILevelFlowComponent
{
    LevelFlowManager owner;
    
    public void FinishSection()
    {
        owner.ProgressFlow();
    }

    public void StartSection(LevelFlowManager flowManager)
    {
        owner = flowManager;
        PlayerInputManager pIM = GameObject.FindGameObjectWithTag("PlayerJoinManager").GetComponent<PlayerInputManager>();

        if(pIM != null)
        {
            if(pIM.playerCount > 0)
            {
                FinishSection();
                return;
                
            }
        }
        GameObject.FindGameObjectWithTag("DontDestroyManager")?.GetComponent<DontDestroyOnLoadManager>().DestroyAllDontDestroyObjects();
        SceneManager.LoadScene("StartScene");

    }
}
