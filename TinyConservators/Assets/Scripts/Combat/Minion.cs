using System;
using System.Collections;
using UnityEngine;

public class Minion : MonoBehaviour, IEatable
{
    [SerializeField] float dieTime;
    [SerializeField] float throwOffForce = 5f;

    [SerializeField] Sprite fallingGoblin;
    [SerializeField] Sprite knockedOutGoblin;

    Enemy mount;

    MinionStates state = MinionStates.knockedOut;
    GameObject owner;
    bool eatable = false;

    public MinionStates State
    {
        get => state;
        set
        {
            if (state != value)
            {
                MinionStates oldState = state;
                state = value;
                OnMinionStateChanged?.Invoke(oldState, state);
            }
        }
    }

    public event Action<MinionStates, MinionStates> OnMinionStateChanged;

    void Start()
    {
        OnMinionStateChanged += (from, to) => StateChanged(from, to);

        ThrowOff();
    }

    void StateChanged(MinionStates from, MinionStates to)
    {
        switch (to)
        {
            case MinionStates.riding:
                break;
            case MinionStates.knockedOut:
                break;
            case MinionStates.projectile:
                GetComponent<GoblinKnockout>().ClearEnumerators();
                TurnIntoProjectile();
                break;
            default:
                break;
        }
    }

    public void Eat(GameObject eater)
    {
        if (eatable)
        {
            gameObject.SetActive(false);
            transform.parent = eater.transform;
            transform.localPosition = Vector2.zero;
        }
    }

    public void SpitOut(GameObject spitter)
    {
        State = MinionStates.projectile;
        owner = spitter;
    }

    void TurnIntoProjectile()
    {
        eatable = false;
        transform.parent = null;
        gameObject.SetActive(true);
    }

    void ProjectileCollision(GameObject collidedObject)
    {
        IDamageReceiver damageObject = collidedObject.GetComponent<IDamageReceiver>();
        if (damageObject != null && collidedObject != owner)
        {
            Debug.Log(collidedObject);
            damageObject.Hurt();
        }
        Die();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (State == MinionStates.projectile)
        {
            ProjectileCollision(collision.gameObject);
        }
        else if (State == MinionStates.knockedOut && collision.gameObject.CompareTag("Platform"))
        {
            eatable = true;
            GetComponentInChildren<SpriteRenderer>().sprite = knockedOutGoblin;
        } 
    }

    void ThrowOff()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        float arcAngle = 22.5f;

        GetComponentInChildren<SpriteRenderer>().sprite = fallingGoblin;

        // Get a random angle in degrees from -22.5 to +22.5
        float randomAngle = UnityEngine.Random.Range(-arcAngle, arcAngle);

        // Convert angle to a direction vector (rotate Vector2.up)
        Vector2 direction = Quaternion.Euler(0, 0, randomAngle) * Vector2.up;

        // Apply force (adjust forceMagnitude to suit your needs)
        
        rb.AddForce(direction * throwOffForce, ForceMode2D.Impulse);


        
    }

    public void GetBackOnMount()
    {
        //Have it set the one it rode when it got knocked off as mount.
        //Transport that mount back in. Set state back to hunting player
        //Destroy minion.

        //Do visual
        mount.transform.position = transform.position;
        mount.Recover();
        Destroy(gameObject);

    }

    public void Die()
    {
        StartCoroutine(die());
        IEnumerator die()
        {
            //Do whatever you need of things to happen here
            
            yield return new WaitForSeconds(dieTime);
            mount.RiderDied();
            Destroy(gameObject);
        }
    }

    //Not sure if this works, but need to know if the player is holding the minion in its mouth, and this seems like a fair way to do it? 
    public void SafeDestroy()
    {
        if (gameObject.activeSelf)
        {
            Destroy(gameObject);
        }
    }

    public void SetMount(Enemy mount)
    {
        this.mount = mount;
    }

    private void OnDestroy()
    {
        OnMinionStateChanged -= StateChanged; //Not completely sure if this is unsubscribing...
    }

    public bool Eatable()
    {
        return eatable;
    }
}

public enum MinionStates
{
    riding,
    knockedOut,
    projectile
}
