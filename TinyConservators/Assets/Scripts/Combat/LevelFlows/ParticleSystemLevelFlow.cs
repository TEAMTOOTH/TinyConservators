using UnityEngine;

public class ParticleSystemLevelFlow : MonoBehaviour, ILevelFlowComponent
{
    [SerializeField] ParticleSystem particles;
    LevelFlowManager owner;
    
    public void FinishSection()
    {
        owner.ProgressFlow();
    }

    public void StartSection(LevelFlowManager flowManager)
    {
        owner = flowManager;
        particles.Play();
        FinishSection();
    }
}
