using System.Collections;
using UnityEngine;

public class GoblinKnockout : MonoBehaviour, IKnockoutable
{
    [SerializeField] float knockOutTime;
    bool knockedOut;
    GameObject owner;
    IEnumerator KnockOutTimer;
    


    public bool IsKnockedOut()
    {
        return knockedOut;
    }

    public void Knockout()
    {
        knockedOut = true;
        
        StartCoroutine(KnockedOut());

    }

    public void PauseKnockout(float pauseTime)
    {
        throw new System.NotImplementedException();
    }

    public void Recover()
    {
        knockedOut = false;
    }

    IEnumerator KnockedOut()
    {
        yield return new WaitForSeconds(knockOutTime);
        GetComponent<Minion>().Die();
    }

    public void ClearEnumerators()
    {
        StopAllCoroutines();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Knockout();
    }
}
