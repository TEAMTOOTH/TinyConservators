using Pathfinding;
using UnityEngine;

//This is a terrible and slow class, should not be used as it sucks. Keeping it here as a warning to anyone later
public class ScanPathfindingContinous : MonoBehaviour
{
    [SerializeField] float rescanTime;
    float timeElapsed;

    AstarPath pathfinder;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pathfinder = GetComponent<AstarPath>();
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        if(timeElapsed > rescanTime)
        {
            pathfinder.Scan();
            rescanTime = 0;
        }
        
    }
}
