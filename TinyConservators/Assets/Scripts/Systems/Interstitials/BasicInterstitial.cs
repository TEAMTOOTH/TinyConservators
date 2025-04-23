using UnityEngine;

public class BasicInterstitial : MonoBehaviour, IInterstitial
{
    [SerializeField] float length;
    public void StartInterstitial()
    {
        Debug.Log("Starting interstitial");
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
