using System.Collections;
using UnityEngine;


public class Pickup : MonoBehaviour, IEatable, IFixer
{
    
    [SerializeField] int pointsValue;
    [SerializeField] float baseLifeTime;
    [SerializeField] float lifeTimeVariaton;
    [SerializeField] bool spittable;
    [SerializeField] bool flyBackSpeed;
    [SerializeField] float amountOfDamageFixedPerPickup;
    [SerializeField] float radiusOfLandingSpot;

    AttackPoint damageOwner;

    bool fixing = false;
    bool eatable = false;

    void Start()
    {
        ChooseColor();
        Invoke("AllowEating", .5f);
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
        GetComponent<Collider2D>().enabled = true;
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




            float moveDuration = 0.6f;
            float time = 0f;

            Vector2 startPos = transform.position;
            //Vector2 endPos = damageOwner.transform.position;

            Vector2 endPos = (Vector2)damageOwner.transform.position + Random.insideUnitCircle * radiusOfLandingSpot;

            // Calculate direction and midpoint
            Vector2 direction = endPos - startPos;
            Vector2 midPoint = (startPos + endPos) / 2f;

            // Create large perpendicular offset for the whip arc
            Vector2 offset = Vector2.Perpendicular(direction).normalized * 10f;
            offset *= Random.value > 0.5f ? 1 : -1;

            Vector2 controlPoint = midPoint + offset;

            // Clamp control point to keep it on screen (orthographic + static camera)
            Camera cam = Camera.main;
            Vector2 screenMin = cam.ViewportToWorldPoint(Vector3.zero);
            Vector2 screenMax = cam.ViewportToWorldPoint(Vector3.one);

            // Keep a 1-unit margin inside screen edges
            float margin = 1f;
            controlPoint.x = Mathf.Clamp(controlPoint.x, screenMin.x + margin, screenMax.x - margin);
            controlPoint.y = Mathf.Clamp(controlPoint.y, screenMin.y + margin, screenMax.y - margin);

            // Animate along Bezier curve with strong acceleration
            while (time < moveDuration)
            {
                time += Time.deltaTime;
                float t = time / moveDuration;

                // Aggressive acceleration for whip feel
                float easedT = Mathf.Pow(t, 4);

                // Quadratic Bezier curve formula
                Vector2 pos = Mathf.Pow(1 - easedT, 2) * startPos +
                              2 * (1 - easedT) * easedT * controlPoint +
                              Mathf.Pow(easedT, 2) * endPos;

                transform.position = pos;

                yield return null;
            }

            transform.position = endPos;


            //Play particle system



            /*

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
            */

            //transform.localScale = endScale;
            damageOwner.FixDamage(amountOfDamageFixedPerPickup);
            Sparkle();
            //Destroy(gameObject);
        }
        

    }

    void Sparkle()
    {
        StartCoroutine(SparkleAndDestroy());
        IEnumerator SparkleAndDestroy()
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            if (GetComponentInChildren<ParticleSystem>() != null)
            {
                GetComponentInChildren<ParticleSystem>().Play();
            }

            yield return new WaitForSeconds(1f);
            Destroy(gameObject);
            
        }
    }
}

