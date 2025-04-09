using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageReceiver
{
    [SerializeField] float knockOutTime;

    bool receiveDamage = true;
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

    void DoDamage()
    {
        throw new NotImplementedException();
    }

    void StartFlying()
    {
        GetComponent<EnemyMovement>().StartMoving();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(receiveDamage && collision.gameObject.CompareTag("Platform"))
        {
            Debug.Log("Im ready to be eaten");
        }
    }

    IEnumerator RecoverFromKnockOut()
    {
        yield return new WaitForSeconds(knockOutTime);
        State = EnemyStates.flying;
    }
}

public enum EnemyStates
{
    flying,
    damaging,
    knockedOut
}
