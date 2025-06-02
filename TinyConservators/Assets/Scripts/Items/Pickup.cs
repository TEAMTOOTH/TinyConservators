using UnityEngine;


public class Pickup : MonoBehaviour, IEatable, IFixer
{
    
    [SerializeField] int pointsValue;
    [SerializeField] float baseLifeTime;
    [SerializeField] float lifeTimeVariaton;
    [SerializeField] bool spittable;

    AttackPoint damageOwner;

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
            Destroy(gameObject);
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
        Destroy(gameObject);
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
        throw new System.NotImplementedException();
    }
}

