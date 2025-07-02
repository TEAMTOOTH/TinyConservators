using UnityEngine;

public class TitleScreenManager : MonoBehaviour
{

    private void Start()
    {
        //Kill all processes. Giving it a fresh start. That includes music, systems and more.
    }
    public void StartGame()
    {
        GameObject.FindGameObjectWithTag("LevelFlow").GetComponent<LevelFlowManager>().ProgressFlow();
    }
}
