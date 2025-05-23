using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterCustomizerInterstitial : MonoBehaviour, IInterstitial
{
    [SerializeField] float length;

    private void Start()
    {
        StartInterstitial();
    }
    public void StartInterstitial()
    {
        GetComponent<SceneLoader>().LoadScene(length);
        Debug.Log("Starting next scene");
    }

    public void EndInterstitial()
    {
        
    }

    public float GetLength()
    {
        return length;
    }

    

    
}
