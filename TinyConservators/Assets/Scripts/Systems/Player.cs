using UnityEngine;

public class Player : MonoBehaviour, IDamageReceiver
{
    [SerializeField] Sprite[] playerSprites; //This is only for testing!
    int playerId;

    public void Initialize(int id)
    {
        playerId = id;

        //This is temp for testing
        GetComponentInChildren<SpriteRenderer>().sprite = playerSprites[id];

    }

    public void Hurt()
    {
        Debug.Log("Hurt called in player");
        GetComponent<IKnockoutable>().Knockout();
    }

    public void SetMoveState(bool state)
    {
        GetComponent<WalkingMovement>().SetCanMove(state);
        GetComponent<FlyingMovement>().SetAllowedToFlap(state);
    }
}
