using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, IDamageReceiver
{
    [SerializeField] Sprite[] playerSprites; //This is only for testing!
    int playerId;
    PlayerStates state = PlayerStates.paused;

    public void Initialize(int id)
    {
        //Randomize players before allowing players to customize.
        Debug.Log("Initialize");

        OnPlayerStateChanged += (from, to) => StateChanged(from, to);

        playerId = id;
        State = PlayerStates.customizing;

        //FullFreeze(true);
        //This is temp for testing
        GetComponentInChildren<SpriteRenderer>().sprite = playerSprites[id];
        


    }

    public PlayerStates State
    {
        get => state;
        set
        {

            if (state != value)
            {
                PlayerStates oldState = state;
                state = value;
                OnPlayerStateChanged?.Invoke(oldState, state);
            }
        }
    }

    public event Action<PlayerStates, PlayerStates> OnPlayerStateChanged;

    void Awake()
    {
        
        
    }

    void StateChanged(PlayerStates from, PlayerStates to)
    {
        Debug.Log("State changed");
        switch (to)
        {
            case PlayerStates.customizing:
                Customize();
                break;
            case PlayerStates.moving:
                AllowMoving();
                break;
            default:
                break;
        }
    }

    public void Hurt()
    {
        Debug.Log("Hurt called in player");
        GetComponent<IKnockoutable>().Knockout();
    }

    public void SetMoveState(bool state)
    {
        GetComponent<WalkingMovement>().SetCanMove(state);
        GetComponent<WalkingMovement>().enabled = state;
        GetComponent<FlyingMovement>().SetAllowedToFlap(state);
        GetComponent<FlyingMovement>().enabled = state;

        if (state)
        {
            GetComponent<PlayerInput>().ActivateInput();
        }
        else
        {
            GetComponent<PlayerInput>().DeactivateInput();
        }
    }

    void AllowMoving()
    {
        Debug.Log("Allowing movement");
        FullFreeze(false);
    }

    public void FullFreeze(bool state)
    {
        SetMoveState(!state);
        if (state)
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
        else
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
        
    }

    //Only a callback for testing, integrate this with the customizer!
    public void DoneCustomizing()
    {
        State = PlayerStates.moving;
        GetComponentInChildren<CharacterCustomizer>().gameObject.GetComponent<PlayerInput>().DeactivateInput();
    }

    public void Customize()
    {
        FullFreeze(true);   
    }
}

public enum PlayerStates
{
    customizing,
    moving,
    paused
}
