using UnityEngine;

public class CallToEndInterstitial : MonoBehaviour
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
}
