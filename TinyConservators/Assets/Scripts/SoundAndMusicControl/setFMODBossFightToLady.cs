using UnityEngine;

public class setFMODBossFightToLady : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("whichBossFight", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
