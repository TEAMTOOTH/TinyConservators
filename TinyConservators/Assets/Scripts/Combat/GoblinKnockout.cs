using UnityEngine;

public class GoblinKnockout : MonoBehaviour, IKnockoutable, IEatable
{
    bool knockedOut;

    public void Eat(GameObject eater)
    {
        Debug.Log("Aaaaah don't eat me!");
        throw new System.NotImplementedException();
    }

    public void SpitOut(GameObject spitter)
    {
        throw new System.NotImplementedException();
    }

    public bool IsKnockedOut()
    {
        return knockedOut;
    }

    public void Knockout()
    {
        knockedOut = true;
    }

    public void PauseKnockout(float pauseTime)
    {
        throw new System.NotImplementedException();
    }

    public void Recover()
    {
        knockedOut = false;
    }

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Knockout();
    }
}
