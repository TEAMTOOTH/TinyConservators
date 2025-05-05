using UnityEngine;

public class IntroStoryInterstitial : MonoBehaviour, IInterstitial
{
    [SerializeField] float length;
    public void StartInterstitial()
    {
        GetComponent<SceneLoader>().LoadScene(length);
    }
    public void EndInterstitial()
    {
        
    }

    public float GetLength()
    {
        return length;
    }

 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartInterstitial();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
