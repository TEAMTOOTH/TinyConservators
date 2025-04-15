using Pathfinding;
using System.Collections;
using UnityEngine;

public class PlayerKnockout : MonoBehaviour, IKnockoutable
{
    [SerializeField] float recoveryTime;

    bool canGetKnockedOut = true;
    bool isKnockedOut = false;
    IEnumerator statusEnumerator;

    void Start()
    {

    }

    public void Knockout()
    {
        if (canGetKnockedOut)
        {
            statusEnumerator = RecoverCountdown();
            StartCoroutine(statusEnumerator);

            GetComponent<Player>().SetMoveState(false);
            isKnockedOut = true;
        }
    }

    public void PauseKnockout(float pauseTime)
    {
        //Maybe write some sort of overwritething here, but hopefully this will do for now.
        canGetKnockedOut = false;
        StartCoroutine(KnockOutPause());
        IEnumerator KnockOutPause()
        {
            float time = 0;
            while (time < pauseTime)
            {
                time += Time.deltaTime;
                yield return null;
            }
            canGetKnockedOut = true;
        }
    }

    public void Recover()
    {
        GetComponent<Player>().SetMoveState(true);
        isKnockedOut = false;
    }

    IEnumerator RecoverCountdown()
    {
        yield return new WaitForSeconds(recoveryTime);
        Recover();
    }

    public bool IsKnockedOut()
    {
        return isKnockedOut;
    }
}
