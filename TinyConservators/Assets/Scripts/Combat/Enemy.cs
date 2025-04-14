using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageReceiver, IEatable
{
    [SerializeField] float knockOutTime;
    [SerializeField] float dieTime;

    GameObject owner;
    bool receiveDamage = true;
    bool eatable;
    EnemyStates state = EnemyStates.flying;

    IEnumerator statusEnumerator;

    public EnemyStates State
    {
        get => state;
        set
        {
            if (state != value)
            {
                EnemyStates oldState = state;
                state = value;
                OnEnemyStateChanged?.Invoke(oldState, state);
            }
        }
    }

    public event Action<EnemyStates, EnemyStates> OnEnemyStateChanged;

    public void Hurt()
    {
        if (receiveDamage)
        {
            State = EnemyStates.knockedOut;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        OnEnemyStateChanged += (from, to) => StateChanged(from, to);
    }

    void StateChanged(EnemyStates from, EnemyStates to) 
    {
        switch (to)
        {
            case EnemyStates.flying:
                StartFlying();
                break;
            case EnemyStates.damaging:
                DoDamage();
                break;
            case EnemyStates.knockedOut:
                KnockOut();
                break;
            case EnemyStates.projectile:
                TurnIntoProjectile();
                break;
            default:
                break;
        }
    }

    void KnockOut()
    {
        GetComponent<EnemyMovement>().KnockOut();
        receiveDamage = false;
        statusEnumerator = RecoverFromKnockOut();
        StartCoroutine(statusEnumerator);

    }

    void TurnIntoProjectile()
    {
        eatable = false;
        transform.parent = null;
        gameObject.SetActive(true);
    }

    void DoDamage()
    {
        throw new NotImplementedException();
    }

    void StartFlying()
    {
        GetComponent<EnemyMovement>().StartMoving();
        
    }

    void Die()
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!receiveDamage && collision.gameObject.CompareTag("Platform") && State != EnemyStates.projectile)
        {
            Debug.Log("Im ready to be eaten");
            eatable = true;
        }
        else if(State == EnemyStates.projectile)
        {
            ProjectileCollision(collision.gameObject);    
        }
    }

    void ProjectileCollision(GameObject collidedObject)
    {
        IDamageReceiver damageObject = collidedObject.GetComponent<IDamageReceiver>();
        if(damageObject != null && collidedObject != owner)
        {
            damageObject.Hurt();
        }
        Die();
    }

    IEnumerator RecoverFromKnockOut()
    {
        yield return new WaitForSeconds(knockOutTime);
        State = EnemyStates.flying;
        receiveDamage = true;
        eatable = false;
        statusEnumerator = null;
    }

    public void Eat(GameObject eater)
    {
        if (eatable)
        {
            gameObject.SetActive(false);
            transform.parent = eater.transform;
            transform.localPosition = Vector2.zero;
            if(statusEnumerator != null)
            {
                StopCoroutine(statusEnumerator);
            }
            
        }
    }

    //Maybe uneccecary obfuscation, but following code standard of the class
    public void SpitOut(GameObject spitter)
    {
        owner = spitter;
        State = EnemyStates.projectile;
    }
}

public enum EnemyStates
{
    flying,
    damaging,
    knockedOut,
    projectile
}
