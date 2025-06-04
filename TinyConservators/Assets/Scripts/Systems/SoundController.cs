using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] AudioClip[] clips;
    AudioSource source;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayClip(int indexOfClip)//Currently using index, cause im rushing for test
    {
        source.clip = clips[indexOfClip];
        source.Play();
    }

    public void Play()
    {
        source.Play();
    }
}
