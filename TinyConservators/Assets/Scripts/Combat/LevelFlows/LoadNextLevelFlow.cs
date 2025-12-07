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

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        //Back up if sucking doesnt work, just going to double up on the ones that already have been transformed.
        for (int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<Player>().ShowVisual(false);
            players[i].GetComponent<Player>().FullFreeze(true);
            players[i].transform.position = new Vector3(0, 0, 0);
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
