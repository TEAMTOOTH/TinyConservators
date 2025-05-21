using UnityEngine;

public class Level1Interstitial : MonoBehaviour, IInterstitial
{
    [SerializeField] float length;
    [SerializeField] AudioClip tickTockIntro;

    public void StartInterstitial()
    {
        gameObject.SetActive(true);
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
