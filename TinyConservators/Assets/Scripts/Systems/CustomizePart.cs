using UnityEngine;

public class CustomizePart : MonoBehaviour
{
    [SerializeField] SpriteRenderer bodyPart;
    [SerializeField] Sprite [] parts;
    [SerializeField] int partIndex;

    float timeElapsed = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bodyPart = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        timeElapsed += Time.deltaTime;
        if(timeElapsed > 1)
        {
            ChangePart(1);
            timeElapsed = 0;
        }*/
    }

    

    /// <summary>
    /// Change the visual part. 
    /// Takes arbritrary number, but usually is expecting 1 to -1. Will roll over to length of parts or 0 if the number exceeds the amount of parts.
    /// </summary>
    /// <param name="direction"></param>
    public void ChangePart(int direction)
    {
        partIndex += direction;
        if(partIndex < 0)
        {
            partIndex = parts.Length - 1;
        }
        else if(partIndex >= parts.Length)
        {
            partIndex = 0;
        }

        bodyPart.sprite = parts[partIndex];
    }

    public void SetPart(int partIndex)
    {
        bodyPart.sprite = parts[partIndex];
    }

    public void SetParts(Sprite[] newParts)
    {
        parts = newParts;
    }
}
