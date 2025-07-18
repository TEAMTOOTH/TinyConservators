using System;
using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour, IDamageReceiver
{
    [SerializeField] float knockOutTime;
    [SerializeField] float dieTime;
    [SerializeField] GameObject goblinVisual;

    float originalSpeed;

    GameObject owner;
    GameObject minion;
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

    public void Hurt(GameObject hurter)
    {
        if (receiveDamage)
        {

            PlayParticleSystemInPlace();
            State = EnemyStates.scatter;
            if(goblinVisual != null)
                goblinVisual.SetActive(false);
            
            GetComponentInChildren<Animator>()?.SetBool("isMounted", false);
            GetComponent<EnemyMovement>().ChangeSpeed(6f);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        OnEnemyStateChanged += (from, to) => StateChanged(from, to);
        originalSpeed = GetComponent<EnemyMovement>().GetCurrentSpeed();
    }

    void StateChanged(EnemyStates from, EnemyStates to) 
    {
        //Debug.Log($"{from} - {to}");
        switch (to)
        {
            case EnemyStates.flying:
                StartFlying();
                break;
            case EnemyStates.damaging:
                DoDamage();
                break;
            case EnemyStates.scatter:
                //GetComponent<IKnockoutable>().Knockout();
                GetComponentInChildren<GoblinSeat>().KnockedOff();
                Scatter();
                break;
            case EnemyStates.projectile:
                TurnIntoProjectile();
                break;
            default:
                break;
        }
    }

    void TurnIntoProjectile()
    {
        SetEatable(false);
        transform.parent = null;
        gameObject.SetActive(true);
    }

    void Scatter()
    {
        //GetClosestScatterPoint();
        //Get closest scatter point.
        GameObject [] scatterPoints = GameObject.FindGameObjectsWithTag("ScatterPoint");

        //GetComponent<CapsuleCollider>().enabled = false;
        gameObject.layer = LayerMask.NameToLayer("NoCollision");

        GameObject closest = null;
        float shortestDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject obj in scatterPoints)
        {
            if (obj == null) continue;

            float distance = Vector3.Distance(currentPosition, obj.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closest = obj;
            }
        }

        GetComponent<EnemyMovement>().SetNewTarget(gameObject);

        if (closest != null)
        {    
            GetComponent<EnemyMovement>().SetLookForTarget(false);    
            GetComponent<EnemyMovement>().SetNewTarget(closest);
        }
    }

    public void InstantDissapear()
    {
        StartCoroutine(Dissapear());
        IEnumerator Dissapear()
        {
            //Do particle effect or smth.
            yield return new WaitForSeconds(.5f);
            if(minion != null)
            {
                minion.GetComponent<Minion>().SafeDestroy();
            }
            Destroy(gameObject);
        }
    }

    public void Recover()
    {
        State = EnemyStates.flying;
        if(goblinVisual != null)
            goblinVisual.SetActive(true);
        GetComponent<EnemyMovement>().SetLookForTarget(true);
        GetComponentInChildren<Animator>()?.SetBool("isMounted", true);
        gameObject.layer = LayerMask.NameToLayer("Enemy");
    }

    void DoDamage()
    {
        throw new NotImplementedException();
    }

    void StartFlying()
    {
        GetComponent<EnemyMovement>().StartMoving();
        GetComponent<EnemyMovement>().ChangeSpeed(originalSpeed);
        

    }

    public void RiderDied()
    {
        if(owner != null)
            owner.GetComponent<IHappenedCounter>()?.ListenedActionHappened();
        
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!receiveDamage && collision.gameObject.CompareTag("Platform") && State != EnemyStates.projectile)
        {
            SetEatable(true);
        }
        else if(State == EnemyStates.projectile)
        {
            //ProjectileCollision(collision.gameObject);    
        }
    }

    void ProjectileCollision(GameObject collidedObject)
    {
        IDamageReceiver damageObject = collidedObject.GetComponent<IDamageReceiver>();
        if(damageObject != null && collidedObject != owner)
        {
            //Debug.Log(collidedObject);
            damageObject.Hurt(collidedObject);
        }
        
        //Die();
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

    void PlayParticleSystemInPlace()
    {
        ParticleSystem ps = GetComponentInChildren<ParticleSystem>();

        if (ps != null)
        {
            ps.transform.parent = null;
            ps.Play();
            StartCoroutine(ReAttach());
            IEnumerator ReAttach()
            {
                yield return new WaitForSeconds(1);
                ps.transform.parent = transform;
                ps.transform.localPosition = Vector2.zero;
            }
        }

    }

    //Maybe uneccecary obfuscation, but following code standard of the class
    public void SpitOut(GameObject spitter)
    {
        State = EnemyStates.projectile;
        owner = spitter;
    }

    public void SetReceiveDamage(bool state)
    {
        receiveDamage = state;
    }

    public void SetEatable(bool state)
    {
        eatable = state;
    }

    public void SetOwner(GameObject owner)
    {
        this.owner = owner;
    }

    public void SetMinion(GameObject minion)
    {
        this.minion = minion;
    }

}

public enum EnemyStates
{
    flying,
    damaging,
    scatter,
    projectile
}
