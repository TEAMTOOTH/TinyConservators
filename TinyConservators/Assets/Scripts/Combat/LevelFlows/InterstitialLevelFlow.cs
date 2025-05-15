using System.Collections;
using UnityEngine;

public class InterstitialLevelFlow : MonoBehaviour, ILevelFlowComponent
{
    [SerializeField] GameObject interstitialGameObject;

    IInterstitial interstitial;
    LevelFlowManager owner;

    void Awake() 
    {
        interstitial = interstitialGameObject.GetComponent<IInterstitial>();
    }

    
    public void FinishSection()
    {
        owner.ProgressFlow();
    }

    public void StartSection(LevelFlowManager flowManager)
    {
        owner = flowManager;
        interstitial.StartInterstitial();

        StartCoroutine(intEnum());
        IEnumerator intEnum()
        {
            yield return new WaitForSeconds(interstitial.GetLength());
            interstitial.EndInterstitial();
            FinishSection();
        }
        
    }
}
