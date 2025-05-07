using System;
using System.Collections;
using UnityEngine;

public class Minion : MonoBehaviour, IEatable
{
    MinionStates state = MinionStates.riding;
    GameObject owner;
    bool eatable = true;
    float dieTime;

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
    }

    public void Die()
    {
        StartCoroutine(die());
        IEnumerator die()
        {
            //Do whatever you need of things to happen here
            yield return new WaitForSeconds(dieTime);
            Destroy(gameObject);
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        OnMinionStateChanged -= StateChanged; //Not completely sure if this is unsubscribing...
    }
}

public enum MinionStates
{
    riding,
    knockedOut,
    projectile
}
