using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class DashMovement : MonoBehaviour
{
    [SerializeField] float dashStrength;
    [SerializeField] float dashSpeedIncrease;

    [SerializeField] float dashDuration;
    [SerializeField] float dashCooldownLength;

    IEnumerator dashCool;

    bool canDash = true;

    public void Dash(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            Eat eat = GetComponentInChildren<Eat>();
            if (eat.IsCarryingFood())
            {
                eat.Spit();
            }
            else
            {
                if (canDash)
                {
                    DashMove();
                }
            }
        }
    }

    void DashMove()
    {
        //Dash
        //GetComponent<Rigidbody2D>().AddForce(new Vector2(direction * dashStrenght))
        WalkingMovement horizontalMovement = GetComponent<WalkingMovement>();

        (float, float) origSpeeds = horizontalMovement.GetSpeedParameters();
        horizontalMovement.SetSpeedParameters((dashStrength, dashSpeedIncrease));
        canDash = false;
        StartCoroutine(DashCooldown());
        IEnumerator DashCooldown()
        {
            float timePassed = 0;
            while (timePassed < dashCooldownLength)
            {
                
                timePassed += Time.deltaTime;
                if (timePassed > dashDuration)
                {
                    horizontalMovement.SetSpeedParameters(origSpeeds);
                }
                
                yield return null;
            }
            canDash = true;
        }
    }

    
}
