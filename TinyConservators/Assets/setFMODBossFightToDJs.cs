using UnityEngine;

public class setFMODBossFightToDJs : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("whichBossFight", 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
