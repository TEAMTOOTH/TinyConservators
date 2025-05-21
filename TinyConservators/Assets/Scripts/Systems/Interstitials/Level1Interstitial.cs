using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class Level1Interstitial : MonoBehaviour, IInterstitial
{
    [SerializeField] float length;
    [SerializeField] float rapStartWait;
    
    //[SerializeField] AudioClip tickTockIntro;

    public void StartInterstitial()
    {
        gameObject.SetActive(true);

        double timeToPlay = AudioSettings.dspTime + rapStartWait; // Play in 0.5s

        GetComponent<AudioSource>().PlayScheduled(timeToPlay);


    }

    public void EndInterstitial()
    {
        gameObject.SetActive(false);
    }

    public float GetLength()
    {
        return length;
    }

    void StartSong()
    {

    }

    
}
