using UnityEngine;

public class BadCharacterBuilder : MonoBehaviour
{
    [SerializeField] Sprite[] bodyParts;

    public Sprite[] ReturnBodyParts()
    {
        return bodyParts;
    }
}
