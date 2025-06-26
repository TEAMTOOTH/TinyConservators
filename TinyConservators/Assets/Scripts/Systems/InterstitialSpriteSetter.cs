using UnityEngine;

public class InterstitialSpriteSetter : MonoBehaviour
{
    [SerializeField] int statToGet;


    [SerializeField] string[] animationStates;
    
    private void Start()
    {
        GameObject g = GameObject.FindGameObjectWithTag("StatTracker");
        if(g != null)
        {
            StatTracker spriteStateInfo = g.GetComponent<StatTracker>();

            
            bool value = spriteStateInfo.GetBossInterstitialState(statToGet);
            Debug.Log("In setSpriteForInterstitial value is: " + value);
            if (value)
            {
                GetComponent<Animator>().Play(animationStates[0]);
            }
            else
            {
                GetComponent<Animator>().Play(animationStates[1]);
            }

        } 
    }
    
}
