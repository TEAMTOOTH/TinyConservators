using UnityEngine;

public class BadPartsGetter : MonoBehaviour
{
    BadCharacterBuilder[] characters;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characters = GetComponentsInChildren<BadCharacterBuilder>();    
    }

    public Sprite[] GetMyColororedBodyParts(int colorIndex)
    {
        return characters[colorIndex].ReturnBodyParts();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
