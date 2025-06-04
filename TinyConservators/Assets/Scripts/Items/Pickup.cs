using System.Collections;
using UnityEngine;


public class Pickup : MonoBehaviour, IEatable, IFixer
{
    
    [SerializeField] int pointsValue;
    [SerializeField] float baseLifeTime;
    [SerializeField] float lifeTimeVariaton;
    [SerializeField] bool spittable;
    [SerializeField] bool flyBackSpeed;
    [Range(0,1)]
    [SerializeField] float amountOfDamageFixedPerPickup;

    AttackPoint damageOwner;

    bool fixing = false;
    bool eatable = false;

    void Start()
    {
        ChooseColor();
        Invoke("AllowEating", 1f);
        Invoke("DestroyAfterSetTime", Random.Range(baseLifeTime - lifeTimeVariaton, baseLifeTime + lifeTimeVariaton));
    }

    public void Eat(GameObject eater)
    {
        if (eatable)
        {
            PointsReceiver pointsReceiver = eater.GetComponent<PointsReceiver>();

            if (pointsReceiver != null)
            {
                pointsReceiver.AddPoints(pointsValue);
            }
            if(damageOwner != null)
            {
                Fix();
            }
            else
            {
                Destroy(gameObject);
            }
            
        }
    }

    public bool Eatable()
    {
        return eatable;
    }

    public void SpitOut(GameObject spitter)
    {
        throw new System.NotImplementedException();
    }

    void ChooseColor()
    {
        Animator colorAnimation = GetComponent<Animator>();
        AnimationClip[] clips = colorAnimation.runtimeAnimatorController.animationClips;

        if (clips.Length == 0) return;

        string clipName = clips[Random.Range(0, clips.Length)].name;

        colorAnimation.Play(clipName);
    }

    void DestroyAfterSetTime()
    {
        //Pop effect?
        if (!fixing)
        {
            Destroy(gameObject);
        }
        
    }

    void AllowEating()
    {
        eatable = true;
    }

    public bool Spittable()
    {
        return spittable;
    }

    public void Consumed(GameObject consumer)
    {
        throw new System.NotImplementedException();
    }

    public void SetEatable(bool state)
    {
        eatable = state;
    }

    public void SetOwner(GameObject objectToFix)
    {
        damageOwner = objectToFix.GetComponent<AttackPoint>();
    }

    public void Fix()
    {
        StartCoroutine(TravelToDamagePoint());
        IEnumerator TravelToDamagePoint()
        {
            GetComponent<SoundController>().Play();

            fixing = true;
            SetEatable(false);
            float time = 0f;
           
            //float moveTime = 1f;

            float speed = .1f;

            Vector2 startPos = transform.position;
            Vector2 currentPos = startPos;
            Vector2 endPos = damageOwner.transform.position;

            while (Vector2.Distance(currentPos, endPos) > 0.1f)
            {
                time += Time.deltaTime;
                //transform.localScale = new Vector3();

                Vector3 basePosition = Vector2.MoveTowards(transform.position, endPos, speed);
                currentPos = basePosition;
                transform.position = basePosition;
                
               
                //Vector3 baseScale = Vector3.Lerp(startScale, endScale, time / moveTime);
                //transform.localScale = baseScale;
                yield return null;
            }

            transform.position = endPos;
            //transform.localScale = endScale;
            damageOwner.FixDamage(amountOfDamageFixedPerPickup);
            Destroy(gameObject);
        }
        

    }
}

