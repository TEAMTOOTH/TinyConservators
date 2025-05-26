using UnityEngine;


public class Pickup : MonoBehaviour, IEatable
{
    
    [SerializeField] int pointsValue;
    [SerializeField] float baseLifeTime;
    [SerializeField] float lifeTimeVariaton;
    [SerializeField] bool spittable;

    void Start()
    {
        ChooseColor();
        Invoke("DestroyAfterSetTime", Random.Range(baseLifeTime - lifeTimeVariaton, baseLifeTime + lifeTimeVariaton));
    }

    public void Eat(GameObject eater)
    {
        PointsReceiver pointsReceiver = eater.GetComponent<PointsReceiver>();

        if(pointsReceiver != null)
        {
            pointsReceiver.AddPoints(pointsValue);
        }

        Destroy(gameObject);

    }

    public bool Eatable()
    {
        return true;
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
        throw new System.NotImplementedException();
    }
}

