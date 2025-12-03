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

            GetComponent<VisualController>().PlayAnimationIfHasState("Hurt");

            GetComponent<Player>().SetMoveState(false);
            gameObject.layer = LayerMask.NameToLayer("Knockout");
            isKnockedOut = true;
            GetComponent<VisualController>().UpdatePart(3);

            // player hurt SFX
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/tinyHurt");
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
        GetComponent<VisualController>().UpdatePart(0);
        gameObject.layer = LayerMask.NameToLayer("Player");
        isKnockedOut = false;
        GetComponent<PlayerCommunication>()?.SendMessage("recover");
        GetComponent<VisualController>().PlayAnimationIfHasState("Float");
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
