using System.Collections;
using UnityEngine;

public class CutsceneMusicController : MonoBehaviour
{
    [SerializeField] AudioClip[] sounds;
    [SerializeField] float[] timings;

    AudioSource source;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        source = GetComponent<AudioSource>();

        if (sounds.Length < 1)
            return;

        PlayClip(sounds[0], 0);
        


    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void PlayClip(AudioClip clip, float length)
    {
        //This is an emergency fix for the kids. Fix later!
        StartCoroutine(PlayStartCutscene());
        IEnumerator PlayStartCutscene()
        {
            source.clip = sounds[0];
            source.Play();
            yield return new WaitForSeconds(timings[0]);

            source.clip = sounds[1];
            source.Play();
            yield return new WaitForSeconds(timings[1]);

            source.clip = sounds[2];
            source.Play();
            yield return new WaitForSeconds(timings[2]);
            
            
        }
    }
}
