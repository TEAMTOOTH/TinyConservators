using UnityEngine;

public class setFMODBossFightToAllOfThem : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("whichBossFight", 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
