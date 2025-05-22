using UnityEngine;

public class AttackBubbleVisual : MonoBehaviour
{
    [SerializeField] float bubbleVariaton;
    
    SpriteRenderer bubbleVisual;
    
    float bubbleMinSize;
    float bubbleMaxSize;

    float originalScale;

    AudioSource bubbleAudio;

    private void Start()
    {
        bubbleVisual = GetComponent<SpriteRenderer>();
        originalScale = transform.localScale.x;
        bubbleAudio = GetComponent<AudioSource>();
    }
    public void StartShowing()
    {
        bubbleVisual.enabled = true;
        bubbleMinSize = transform.localScale.x - bubbleVariaton;
        bubbleMaxSize = transform.localScale.x + bubbleVariaton;

        
        if(bubbleAudio != null)
        {
            bubbleAudio.Play();
        }
    }
    
    public void ChangeBubbleSize(float time, float totalTime)
    {
        //Do the sizechanging here
        float size = Mathf.Lerp(bubbleMinSize, bubbleMaxSize, time/totalTime);
        transform.localScale = new Vector3(size, size, size);
    }

    public void PopBubble()
    {
        bubbleVisual.enabled = false;
        transform.localScale = new Vector3(originalScale, originalScale, originalScale);
        bubbleAudio.Stop();
    }
}
