using UnityEngine;

[RequireComponent(typeof(SceneLoader))]
public class LoadNextLevelFlow : MonoBehaviour, ILevelFlowComponent
{
    [SerializeField] float loadLength;
    [SerializeField] int levelNumberForStats;
    public void FinishSection()
    {
        GetComponent<SceneLoader>().LoadScene(loadLength);
    }

    public void StartSection(LevelFlowManager flowManager)
    {
        GameObject g = GameObject.FindGameObjectWithTag("StatTracker");
        if(g != null)
        {
            g.GetComponent<StatTracker>().SetStats(levelNumberForStats);
        }

        FinishSection();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
