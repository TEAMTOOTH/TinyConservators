using Pathfinding;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using static UnityEngine.Rendering.DebugUI;

public class EnemyKnockout : MonoBehaviour, IKnockoutable
{
    [SerializeField] float recoveryTime;
    Rigidbody2D rb2D;
    IEnumerator statusEnumerator;
    bool canGetKnockedOut = true;
    bool isKnockedOut = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    void IKnockoutable.Knockout()
    {
        if (canGetKnockedOut)
        {
            GetComponent<Enemy>().SetReceiveDamage(false);
            statusEnumerator = RecoverCountdown();
            StartCoroutine(statusEnumerator);

            GetComponent<EnemyMovement>().SetCanMove(false);
            GetComponent<AIPath>().enabled = false;
            rb2D.bodyType = RigidbodyType2D.Dynamic;
            rb2D.freezeRotation = false;
            isKnockedOut = true;
        }
    }



    public void Recover()
    {
        GetComponent<EnemyMovement>().StartMoving();
        Enemy e = GetComponent<Enemy>();
        e.State = EnemyStates.flying; //Is it right to set this from here? Probably not, don't love it, but also can't think of a better way currently.
        e.SetReceiveDamage(true);
        e.SetEatable(false);
        statusEnumerator = null;
        isKnockedOut = false;
    }

    IEnumerator RecoverCountdown()
    {
        yield return new WaitForSeconds(recoveryTime);
        Recover();
    }

    public void PauseKnockout(float pauseTime)
    {
        canGetKnockedOut = false;
        StartCoroutine(KnockOutPause());
        IEnumerator KnockOutPause()
        {
            float time = 0;
            while(time < pauseTime)
            {
                time += Time.deltaTime;
                yield return null;
            }
            canGetKnockedOut = true;
        }

    }

    public bool IsKnockedOut()
    {
        return isKnockedOut;
    }
}
