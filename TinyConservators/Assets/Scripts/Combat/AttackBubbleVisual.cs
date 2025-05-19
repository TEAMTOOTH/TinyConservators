using UnityEngine;

public class AttackBubbleVisual : MonoBehaviour
{
    [SerializeField] float bubbleVariaton;
    
    SpriteRenderer bubbleVisual;
    
    float bubbleMinSize;
    float bubbleMaxSize;

    float originalScale;
    

    private void Start()
    {
        bubbleVisual = GetComponent<SpriteRenderer>();
        originalScale = transform.localScale.x;
    }
    public void StartShowing()
    {
        bubbleVisual.enabled = true;
        bubbleMinSize = transform.localScale.x - bubbleVariaton;
        bubbleMaxSize = transform.localScale.x + bubbleVariaton;
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
    }
}
