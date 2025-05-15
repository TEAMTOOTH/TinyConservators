using UnityEngine;

public class Level1Interstitial : MonoBehaviour, IInterstitial
{
    [SerializeField] float length;

    public void StartInterstitial()
    {
    
    }

    public void EndInterstitial()
    {
        gameObject.SetActive(false);
    }

    public float GetLength()
    {
        return length;
    }

    
}
