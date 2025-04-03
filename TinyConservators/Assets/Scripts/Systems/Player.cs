using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Sprite[] playerSprites; //This is only for testing!
    int playerId;

    public void Initialize(int id)
    {
        playerId = id;

        //This is temp for testing
        GetComponentInChildren<SpriteRenderer>().sprite = playerSprites[id];

    }
}
