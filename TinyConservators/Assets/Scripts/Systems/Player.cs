using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, IDamageReceiver
{
    [SerializeField] Sprite[] playerSprites; //This is only for testing!
    //[SerializeField] GameObject colorExpulsionPoint;
    [SerializeField] Vector3 colorExpulsionOffset;
    int playerId;
    PlayerStates state = PlayerStates.paused;

    VisualController playerVisuals;
    public void Initialize(int id)
    {
        //Randomize players before allowing players to customize.
        Debug.Log("Initialize");

        DontDestroyOnLoad(gameObject);

        OnPlayerStateChanged += (from, to) => StateChanged(from, to);

        playerId = id;
        
        //State = PlayerStates.customizing;

        playerVisuals = GetComponent<VisualController>();
        
        //This is temp for testing
        GetComponentInChildren<CharacterCustomizer>().Initialize(id);
        


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
            case PlayerStates.paused:
                LockPlayerIn();
                break;
            default:
                break;
        }
    }

    public void Hurt(GameObject hurter)
    {
        GetComponent<IKnockoutable>().Knockout();

        GetComponent<SoundController>().PlayClip(0);
    }

    public void SetMoveState(bool state)
    { 
        GetComponent<WalkingMovement>().SetCanMove(state); //Don't know if I need these, clean up if you have the time to check.
        GetComponent<WalkingMovement>().enabled = state;
        
        GetComponent<FlyingMovement>().SetAllowedToFlap(state); //Don't know if I need these, clean up if you have the time to check.
        GetComponent<FlyingMovement>().enabled = state;
        
        //GetComponent<PlayerInput>().enabled = state;
        
        
    }

    void AllowMoving()
    {
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
            //playerVisuals.UpdatePart(0);
        }
        
    }

    //Only a callback for testing, integrate this with the customizer!
    public void DoneCustomizing()
    {
        //State = PlayerStates.moving;
        //GetComponentInChildren<CharacterCustomizer>().enabled = false;
    }

    public void PauseMovement(float time)
    {
        StartCoroutine(PM());
        IEnumerator PM ()
        {
            SetMoveState(false);
            yield return new WaitForSeconds(time);
            SetMoveState(true);
        }
    }

    public void Customize()
    {
        FullFreeze(true);
        
    }

    public void LockPlayerIn()
    {
        FullFreeze(true);
    }

    public void AnimationTransition(int from, int to, float time)
    {
        StartCoroutine(eat());
        IEnumerator eat()
        {
            GetComponentInParent<VisualController>().UpdatePart(from);
            yield return new WaitForSeconds(time);
            GetComponentInParent<VisualController>().UpdatePart(to);
        }
    }

    public void ShowVisual(bool state)
    {
        GetComponentInChildren<SpriteRenderer>().enabled = state;
    }

    //Not sure what this is?
    public void DebugStartAction()
    {
        if(State == PlayerStates.moving)
        {
            GameObject.FindGameObjectWithTag("Interstitial").GetComponent<IInterstitial>().StartInterstitial();
        }
    }

    public int GetPlayerId()
    {
        return playerId;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Platform"))
        {
            PlayGroundHitParticleSystem();
        }
    }

    void PlayGroundHitParticleSystem()
    {
        ParticleSystem particleSystem = GetComponentInChildren<ParticleSystem>();
        if (particleSystem != null)
            {
                particleSystem.Play();
            }
    }

    public Vector3 GetColorExpulsionPoint()
    {
        Debug.Log(GetComponent<WalkingMovement>().GetDirection() + ", " + transform.position.x + (colorExpulsionOffset.x * GetComponent<WalkingMovement>().GetDirection()));
        Vector3 returnVector = new Vector3(transform.position.x + (colorExpulsionOffset.x * GetComponent<WalkingMovement>().GetDirection()), transform.position.y, transform.position.z);
        return returnVector;
    }
}


public enum PlayerStates
{
    customizing,
    moving,
    paused
}
